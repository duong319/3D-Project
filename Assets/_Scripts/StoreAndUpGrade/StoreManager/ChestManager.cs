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
        chestOpenPanel = GetComponent<ChestOpenPanel>();
    }

    #region Open Chest
    public void OpenChest(ChestData chest)
    {
        List<Reward> rewards = RollRewards(chest);
        AudioManager.Instance.Play("ChestOpen");
        foreach (var reward in rewards)
        {
            GrantReward(reward);
        }

        chestOpenPanel.ShowChestOpenAnimation(rewards, chest);
        AchievementManager.Instance.AddProgress(AchievementType.Openbox, 1);
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
                CurrencyManager.Instance.AddTotalExp(amount);
                break;
            case RewardType.HeadStart:
                CurrencyManager.Instance.AddHeadStart(amount);
                break;
            case RewardType.ScoreBooster:
                CurrencyManager.Instance.AddScoreBooster(amount);
                break;
        }
        AudioManager.Instance.Play("Claim");
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
        if (IsVideoChestAvailableToday())
        {
            OpenChest(videoChestData);
            lastVideoChestDay = DateTime.Today;
            SaveVideoChestDay();

            FreeBtn.gameObject.SetActive(false);
            WatchAdBtn.gameObject.SetActive(true);
        }
        else
        {
            RewardedAdsButton.Instance.LoadAd(Rewardtype.None);
            RewardedAdsButton.Instance.onAdCompleted = () =>
            {
                OpenChest(videoChestData);
            };
        }
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
        }


        if (IsVideoChestAvailableToday())
        {
            FreeBtn.gameObject.SetActive(true);
            WatchAdBtn.gameObject.SetActive(false);
        }
        else
        {
            FreeBtn.gameObject.SetActive(false);
            WatchAdBtn.gameObject.SetActive(true);
        }
    }
    #endregion

    public void OnClick_OpenVideoChest()
    {
        AudioManager.Instance.Play("Btn");
        Debug.Log("Normal");
        ClaimVideoChestToday();
    }

    public void OnClick_OpenNormalChest()
    {
        AudioManager.Instance.Play("Btn");
        Debug.Log("Normal");
        if (CurrencyManager.Instance.Coins >= 100)
        {
            CurrencyManager.Instance.SpendCoins(100);
            OpenChest(normalChestData);
        }
    }

    public void OnClick_OpenRareChest()
    {
        AudioManager.Instance.Play("Btn");
        Debug.Log("rare");
        if (CurrencyManager.Instance.Gems >= 1)
        {
            CurrencyManager.Instance.SpendGems(1);
            OpenChest(rareChestData);
        }
    }

}
