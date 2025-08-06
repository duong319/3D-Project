using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static ChestManager Instance;

    public ChestData videoChestData;
    public ChestData normalChestData;
    public ChestData rareChestData;

    public GameObject FreeBtn;
    public GameObject WatchAdBtn;
    public ChestOpenPanel chestOpenPanel;

    private DateTime lastVideoChestDay;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        LoadVideoChestDay();
        chestOpenPanel=GetComponent<ChestOpenPanel>();
    }

    #region Open Chest
    public void OpenChest(ChestData chest)
    {
        List<Reward> rewards = RollRewards(chest);
        foreach (var reward in rewards)
        {
            GrantReward(reward);
        }

        chestOpenPanel.ShowChestOpenAnimation(rewards,chest);
    }

    List<Reward> RollRewards(ChestData chest)
    {
        List<Reward> results = new();
        int count = Mathf.Min(chest.rewardCount, chest.rewards.Count);

        for (int i = 0; i < count; i++)
        {
            Reward reward = RollReward(chest, results);
           
            results.Add(reward);
        }

        return results;
    }

    Reward RollReward(ChestData chest, List<Reward> exclude)
    {
        float total = 0f;
        foreach (var r in chest.rewards)
        {
            if (!exclude.Contains(r)) total += r.chance;
        }

        float roll = UnityEngine.Random.Range(0, total);
        float cumulative = 0f;

        foreach (var reward in chest.rewards)
        {
            if (exclude.Contains(reward)) continue;

            cumulative += reward.chance;
            if (roll <= cumulative)
                return reward;
        }

      
        return chest.rewards.Find(r => !exclude.Contains(r));
    }


    void GrantReward(Reward reward)
    {
        int amount = reward.GetRandomAmount();
        switch (reward.type)
        {
            case RewardType.Coins:
                CurrencyManager.Instance.AddCoins(amount);
                break;
            case RewardType.Gems:
                CurrencyManager.Instance.AddGems(amount);
                break;
            case RewardType.Exp:
                CurrencyManager.Instance.AddExp(amount);
                break;
            case RewardType.HeadStart:
                CurrencyManager.Instance.AddHeadStart(amount);
                break;
            case RewardType.ScoreBooster:
                CurrencyManager.Instance.AddScoreBooster(amount);
                break;
        }

        Debug.Log($"Granted: {reward.name} x{amount}");
    }
    #endregion

    #region Daily Free Video Chest
    public bool IsVideoChestAvailableToday()
    {
        return lastVideoChestDay.Date != DateTime.Today;
    }

    public void ClaimVideoChestToday()
    {
        if (!IsVideoChestAvailableToday()) return;

        OpenChest(videoChestData);
        FreeBtn.gameObject.SetActive(false);
        WatchAdBtn.gameObject.SetActive(true);
        lastVideoChestDay = DateTime.Today;
        SaveVideoChestDay();
    }

    void SaveVideoChestDay()
    {
        PlayerPrefs.SetString("LastVideoChestDay", lastVideoChestDay.ToString("yyyy-MM-dd"));
    }

    void LoadVideoChestDay()
    {
        if (PlayerPrefs.HasKey("LastVideoChestDay"))
        {
            lastVideoChestDay = DateTime.Parse(PlayerPrefs.GetString("LastVideoChestDay"));
        }
        else
        {
            lastVideoChestDay = DateTime.Today.AddDays(-1);
            FreeBtn.gameObject.SetActive(true);
            WatchAdBtn.gameObject.SetActive(false);
        }
    }
    #endregion

    public void OnClick_OpenVideoChest()
    {
        Debug.Log("Normal");
        if (IsVideoChestAvailableToday())
        {
            // TODO: Call AD
            ClaimVideoChestToday();
        }
        else
        {
            Debug.Log("Already claimed today's Video Chest!");
        }
    }

    public void OnClick_OpenNormalChest()
    {
        Debug.Log("Normal");
        if (CurrencyManager.Instance.Coins >= 1000)
        {
            CurrencyManager.Instance.SpendCoins(1000);
            OpenChest(normalChestData);
        }
    }

    public void OnClick_OpenRareChest()
    {
        Debug.Log("rare");
        if (CurrencyManager.Instance.Gems >= 10)
        {
            CurrencyManager.Instance.SpendGems(10);
            OpenChest(rareChestData);
        }
    }

}
