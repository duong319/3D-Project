using System.Collections;
using System.Collections.Generic;
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

    public void UpdateTier(int currentExp)
    {
        bool isClaimed = tierData.claimed;
        progressSliderClaimed.SetActive(isClaimed);
        claimedMark.SetActive(isClaimed);
        claimButton.gameObject.SetActive(!isClaimed && currentExp >= tierData.requiredLevel);
    }

    void ClaimReward()
    {
        DailyScoreManager.Instance.ClaimReward(tierIndex);
        UpdateTier(DailyScoreManager.Instance.todayHighScore);
    }
}
