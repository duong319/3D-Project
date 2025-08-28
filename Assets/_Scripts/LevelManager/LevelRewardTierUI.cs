
using UnityEngine;
using UnityEngine.UI;

public class LevelRewardTierUI : MonoBehaviour
{
    public int tierIndex;
    public GameObject progressSliderClaimed;
    public Button claimButton;
    public GameObject claimedMark;

    private LevelRewardData.LevelRewardTier tierData => LevelRewardManager.Instance.rewardData.LevelrewardTiers[tierIndex];

    private void Start()
    {
        claimButton.onClick.AddListener(ClaimReward);
    }

    public void UpdateTier(int currentLevel)
    {
        bool isClaimed = tierData.claimed;
        progressSliderClaimed.SetActive(isClaimed);
        claimedMark.SetActive(isClaimed);
        claimButton.gameObject.SetActive(!isClaimed && currentLevel >= tierData.requiredLevel);
        Debug.Log(isClaimed);
    }

    void ClaimReward()
    {    
        AudioManager.Instance.Play("Claim");
        LevelRewardManager.Instance.CheckAndGiveRewards();
        UpdateTier(CurrencyManager.Instance.PlayerLevel);
        LevelRewardManager.Instance.RefreshAllUI();
        Debug.Log("Claim");
    }
}
