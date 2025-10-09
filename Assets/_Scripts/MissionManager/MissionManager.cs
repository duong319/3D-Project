using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public List<MissionData> missionDatas;
    public List<Mission> currentMissions = new List<Mission>();

    public int maxMissions = 3;
    public bool isRewardClaim = false;

    private const string SaveKey = "MissionProgress";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadMissions();
    }

    void LoadMissions()
    {
        currentMissions.Clear();

        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            var wrapper = JsonUtility.FromJson<MissionSaveWrapper>(json);

            foreach (var save in wrapper.items)
            {
                var data = missionDatas.FirstOrDefault(m => m.name == save.missionID);
                if (data != null)
                {
                    currentMissions.Add(new Mission
                    {
                        data = data,
                        currentAmount = save.currentAmount
                    });
                }
            }
        }
        else
        {
            var shuffled = new List<MissionData>(missionDatas);
            shuffled.Sort((a, b) => Random.Range(-1, 2));

            for (int i = 0; i < maxMissions && i < shuffled.Count; i++)
            {
                currentMissions.Add(new Mission
                {
                    data = shuffled[i],
                    currentAmount = 0
                });
            }
        }
    }

    public void ReportProgress(MissionType type, int amount)
    {
        foreach (var mission in currentMissions)
        {
            if (mission.data.missionType == type && !mission.isCompleted)
            {
                mission.currentAmount += amount;
            }
        }
        SaveMissions();
    }

    public void SaveMissions()
    {
        List<MissionSaveData> saveList = new List<MissionSaveData>();

        foreach (var mission in currentMissions)
        {
            saveList.Add(new MissionSaveData
            {
                missionID = mission.data.name,
                currentAmount = mission.currentAmount
            });
        }

        string json = JsonUtility.ToJson(new MissionSaveWrapper(saveList));
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void ClaimReward(Mission mission)
    {
        if (!currentMissions.All(m => m.isCompleted)) return;
        CurrencyManager.Instance.AddExp(75);
        CurrencyManager.Instance.AddTotalExp(75);
        CheckAllCompleted();
    }

    public void SkipMission(Mission mission)
    {
        if (CurrencyManager.Instance.Coins < mission.data.skipCost)
        {
            Debug.Log("not enough coin");
            return;
        }
        CurrencyManager.Instance.SpendCoins(mission.data.skipCost);
        mission.currentAmount = mission.data.targetAmount;
        CurrencyManager.Instance.AddExp(mission.data.rewardExp);
        CurrencyManager.Instance.AddTotalExp(mission.data.rewardExp);
        SaveMissions();
        CheckAllCompleted();    
    }

    void CheckAllCompleted()
    {
        if (currentMissions.All(m => m.isCompleted))
        {
            AudioManager.Instance.Play("Claim");
            CurrencyManager.Instance.AddscoreMultiplier(1);
            GenerateNewMissions();
            SaveMissions();
            FindFirstObjectByType<MissionPanel>().ShowMissionPanel();
            StartCoroutine(ResetClaim());
        }
    }

    void GenerateNewMissions()
    {
        var oldMissionIDs = currentMissions.Select(m => m.data.name).ToHashSet();

        var available = missionDatas.Where(m => !oldMissionIDs.Contains(m.name)).ToList();

        if (available.Count < maxMissions)
        {
            available = new List<MissionData>(missionDatas);
        }

        available.Sort((a, b) => Random.Range(-1, 2));

        currentMissions.Clear();

        for (int i = 0; i < maxMissions && i < available.Count; i++)
        {
            currentMissions.Add(new Mission
            {
                data = available[i],
                currentAmount = 0
            });
        }
    }

    private IEnumerator ResetClaim()
    {
        isRewardClaim = true;
        yield return new WaitForSeconds(1.9f);
        isRewardClaim = false;
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("MissionProgress");
    }


    private void OnApplicationQuit()
    {
        SaveMissions();
    }

    [System.Serializable]
    public class MissionSaveData
    {
        public string missionID;
        public int currentAmount;
    }

    [System.Serializable]
    public class MissionSaveWrapper
    {
        public List<MissionSaveData> items;

        public MissionSaveWrapper(List<MissionSaveData> list)
        {
            items = list;
        }
    }
}
