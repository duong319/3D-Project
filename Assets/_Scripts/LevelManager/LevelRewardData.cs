using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelReward", menuName = "Level/Reward")]
public class LevelRewardData : ScriptableObject
{
    public List<LevelRewardTier> LevelrewardTiers;

    [System.Serializable]
    public class LevelRewardTier
    {
        public int requiredLevel;
        public int Reward;
        public RewardQuality rewardQuality;
        public bool claimed;
    }

    public enum RewardQuality
    {  
       Scoremultiplier,
       Box,
       CharacterUnlock,
    }
}
