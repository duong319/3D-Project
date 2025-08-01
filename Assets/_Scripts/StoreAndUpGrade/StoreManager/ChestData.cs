using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewChestData", menuName = "Chest/ChestData")]
public class ChestData : ScriptableObject
{
    public string chestName;
    public int rewardCount = 2;
    public Sprite chestIcon;
    public List<Reward> rewards;
}


[System.Serializable]
public class Reward
{
    public string name;
    public RewardType type;
    public Sprite icon;
    public int minAmount;
    public int maxAmount;
    public float chance; 

    public int GetRandomAmount()
    {
        return UnityEngine.Random.Range(minAmount, maxAmount + 1);
    }
}

public enum RewardType
{
    Coins,
    Gems,
    Exp,
    HeadStart, 
    ScoreBooster
}
