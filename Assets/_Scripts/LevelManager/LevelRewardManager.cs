using UnityEngine;
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
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadClaimedStatus();
    }

    private void LoadClaimedStatus()
    {
        foreach (var tier in rewardData.LevelrewardTiers)
        {
            tier.claimed = PlayerPrefs.GetInt(ClaimedKey + tier.requiredLevel, 0) == 1;
        }
    }


    public void GiveReward(LevelRewardData.LevelRewardTier tier)
    {
        Debug.Log($"Reward for level {tier.requiredLevel}: {tier.rewardQuality} (+{tier.Reward})");
        CurrencyManager.Instance.AddscoreMultiplier(tier.Reward);
        switch (tier.rewardQuality)
        {
            case LevelRewardData.RewardQuality.Box:

                ChestManager.Instance.OpenChest(normalchest);
                break;

            case LevelRewardData.RewardQuality.CharacterUnlock:
                break;
        }
    }


    public void MarkRewardClaimed(int level)
    {
        PlayerPrefs.SetInt(ClaimedKey + level, 1);
        PlayerPrefs.Save();
        var tier = rewardData.LevelrewardTiers.Find(t => t.requiredLevel == level);
        if (tier != null) tier.claimed = true;
    }
    public void RefreshAllUI()
    {
        foreach (var tierUI in FindObjectsOfType<LevelRewardTierUI>())
        {
            tierUI.UpdateTier(CurrencyManager.Instance.PlayerLevel);
        }
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
