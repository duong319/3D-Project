using System.Collections.Generic;
using UnityEngine;

public class DailyScoreManager : MonoBehaviour
{
    public static DailyScoreManager Instance;

    public DailyScoreRewardData rewardData;
    public int todayHighScore;

    private const string LastResetKey = "LastDailyReset";
    private const string ClaimedKeyPrefix = "DailyScore_Claimed_";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        CheckReset();
        LoadTodayHighScore();
        LoadClaimedStatus();
    }

    public void UpdateHighScore(int score)
    {
        if (score > todayHighScore)
        {
            todayHighScore = score;
            PlayerPrefs.SetInt("DailyHighScore", todayHighScore);
        }
    }

    public void ClaimReward(int tierIndex)
    {
        var tier = rewardData.rewardTiers[tierIndex];

        if (tier.claimed || todayHighScore < tier.requiredScore)
        {
            return;
        }


        CurrencyManager.Instance.AddCoins(tier.coins);


        switch (tier.specialItemType)
        {
            case DailyScoreRewardData.SpecialItemType.HeadStart:
                CurrencyManager.Instance.AddHeadStart(tier.SpecialItems);
                break;
            case DailyScoreRewardData.SpecialItemType.ScoreBooster:
                CurrencyManager.Instance.AddScoreBooster(tier.SpecialItems);
                break;

        }

        tier.claimed = true;
        PlayerPrefs.SetInt(ClaimedKeyPrefix + tierIndex, 1);
        PlayerPrefs.Save();

        Debug.Log("Claimed " + tierIndex);
    }

    public bool IsRewardClaimed(int tierIndex)
    {
        return rewardData.rewardTiers[tierIndex].claimed;
    }

    void LoadTodayHighScore()
    {
        todayHighScore = PlayerPrefs.GetInt("DailyHighScore", 0);
    }

    void LoadClaimedStatus()
    {
        for (int i = 0; i < rewardData.rewardTiers.Count; i++)
        {
            rewardData.rewardTiers[i].claimed = PlayerPrefs.GetInt(ClaimedKeyPrefix + i, 0) == 1;
        }
    }

    public void ResetDailyData()
    {
        todayHighScore = 0;
        PlayerPrefs.SetInt("DailyHighScore", 0);

        for (int i = 0; i < rewardData.rewardTiers.Count; i++)
        {
            rewardData.rewardTiers[i].claimed = false;
            PlayerPrefs.DeleteKey(ClaimedKeyPrefix + i);
        }

        PlayerPrefs.Save();
    }


    void CheckReset()
    {
        string lastDate = PlayerPrefs.GetString(LastResetKey, "");
        if (lastDate != System.DateTime.Now.ToString("yyyyMMdd"))
        {
            foreach (var r in rewardData.rewardTiers)
            {
                r.claimed = false;
            }

            PlayerPrefs.SetString(LastResetKey, System.DateTime.Now.ToString("yyyyMMdd"));
            PlayerPrefs.SetInt(LastResetKey, 0); 
            PlayerPrefs.Save();
        }
    }
}
