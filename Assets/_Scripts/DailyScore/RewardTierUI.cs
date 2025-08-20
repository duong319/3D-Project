using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardTierUI : MonoBehaviour
{
    public int tierIndex;

  
    public GameObject progressSliderClaimed;
    public Button claimButton;
    public GameObject claimedMark;

    private DailyScoreRewardData.RewardTier tierData => DailyScoreManager.Instance.rewardData.rewardTiers[tierIndex];

    private void Start()
    {
        claimButton.onClick.AddListener(ClaimReward);
       
    }

    public void UpdateTier(int currentScore)
    {     
        bool isClaimed = tierData.claimed;
        progressSliderClaimed.SetActive(isClaimed);
        claimedMark.SetActive(isClaimed);
        claimButton.gameObject.SetActive(!isClaimed && currentScore >= tierData.requiredScore);
    }

    void ClaimReward()
    {
        AudioManager.Instance.Play("Claim");
        DailyScoreManager.Instance.ClaimReward(tierIndex);
        UpdateTier(DailyScoreManager.Instance.todayHighScore);
    }
}
