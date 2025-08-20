using UnityEngine;
using System.Collections.Generic;

public class LevelRewardManager : MonoBehaviour
{
    public static LevelRewardManager Instance;
    public LevelRewardData rewardData;
    public ChestData normalchest;


    private const string ClaimedKey = "LevelReward_Claimed_";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //CheckAndGiveRewards();
            ResetAllClaims();
        }
        else
        {
            Destroy(gameObject);
        }

    }
  

 
    public void CheckAndGiveRewards()
    {
      
        foreach (var tier in rewardData.LevelrewardTiers)
        {
           
            if (CurrencyManager.Instance.PlayerLevel >= tier.requiredLevel && !IsRewardClaimed(tier.requiredLevel))
            {
                Debug.Log("check");
                GiveReward(tier);
                MarkRewardClaimed(tier.requiredLevel);
            }
        }
    }

    private void GiveReward(LevelRewardData.LevelRewardTier tier)
    {
        Debug.Log($"Reward for level {tier.requiredLevel}: {tier.rewardQuality} (+{tier.Reward})");
        CurrencyManager.Instance.AddscoreMultiplier(tier.Reward);
        switch (tier.rewardQuality)
        {       
            case LevelRewardData.RewardQuality.Box:

                // Inventory.Instance.AddBox(tier.Reward);
                ChestManager.Instance.OpenChest(normalchest);
                break;

            case LevelRewardData.RewardQuality.CharacterUnlock:
                
               // CharacterManager.Instance.UnlockCharacter(tier.Reward);
                break;
        }
    }

    private bool IsRewardClaimed(int level)
    {
        return PlayerPrefs.GetInt(ClaimedKey + level, 0) == 1;
    }

    private void MarkRewardClaimed(int level)
    {
        PlayerPrefs.SetInt(ClaimedKey + level, 1);
        PlayerPrefs.Save();
        var tier = rewardData.LevelrewardTiers.Find(t => t.requiredLevel == level);
        if (tier != null) tier.claimed = true;
    }

    public void ResetAllClaims() 
    {
        Debug.Log("Reset");
        foreach (var tier in rewardData.LevelrewardTiers)
        {
            PlayerPrefs.DeleteKey(ClaimedKey + tier.requiredLevel);
        }
        PlayerPrefs.Save();
    }
}
