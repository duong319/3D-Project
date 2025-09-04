using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyScoreReward", menuName = "Daily/Score Reward")]
public class DailyScoreRewardData : ScriptableObject
{
    public List<RewardTier> rewardTiers;

    [System.Serializable]
    public class RewardTier
    {
        public int requiredScore;
        public int coins;
        public int SpecialItems;
        public SpecialItemType specialItemType;
        public bool claimed;
    }

    public enum SpecialItemType
    {
        None,
        HeadStart,
        ScoreBooster
    }
}
