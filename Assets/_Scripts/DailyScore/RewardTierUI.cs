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
      

        float progress = Mathf.Clamp01((float)currentScore / tierData.requiredScore);
       

        bool isClaimed = tierData.claimed;
        claimedMark.SetActive(isClaimed);
        claimButton.gameObject.SetActive(!isClaimed && currentScore >= tierData.requiredScore);
    }

    void ClaimReward()
    {
        DailyScoreManager.Instance.ClaimReward(tierIndex);
        UpdateTier(DailyScoreManager.Instance.todayHighScore);
    }
}
