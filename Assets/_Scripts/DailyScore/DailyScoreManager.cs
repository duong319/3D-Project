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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadClaimedStatus();
            CheckReset();
            todayHighScore = PlayerPrefs.GetInt("DailyHighScore", 0);
        }
        else
        {
            Destroy(gameObject);
        }

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


    public void CheckReset()
    {
        string lastDate = PlayerPrefs.GetString(LastResetKey, "");
        string today = System.DateTime.Now.ToString("yyyyMMdd");

        Debug.Log("LastResetKey = " + lastDate + ", Today = " + today);
        if (lastDate != today)
        {
            Debug.Log("Reset");
            foreach (var r in rewardData.rewardTiers)
            {
                r.claimed = false;
            }

            todayHighScore = 0;
            PlayerPrefs.SetInt("DailyHighScore", 0);
            PlayerPrefs.SetString(LastResetKey, today);       
            PlayerPrefs.Save();
        }
    }

    public DailyScoreRewardData.RewardTier GetNextTier()
    {
        foreach (var tier in rewardData.rewardTiers)
        {
            if (!tier.claimed)
            {
                return tier;
            }
        }
        return null;
    }

    public int GetRemainingScore()
    {
        var nextTier = GetNextTier();
        if (nextTier == null) return 0;
        return Mathf.Max(0, nextTier.requiredScore - ScoreManager.Instance.lastScore);
    }

    public System.TimeSpan GetTimeUntilReset()
    {

        System.DateTime now = System.DateTime.Now;


        System.DateTime nextReset = now.Date.AddDays(1);

        return nextReset - now;
    }
}
