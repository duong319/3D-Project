using UnityEngine;

public enum AchievementType
{
    CollectCoins,
    SpendGem,
    PickupItem,
    PlayCount,
    WatchAd,
    Jump,
    Openbox,
}

[CreateAssetMenu(fileName = "AchievementData", menuName = "Game/Achievement")]
public class AchievementData : ScriptableObject
{
    public string id;
    public string title;
    public string description;
    public Sprite icon;
    public AchievementType type;
    public int targetValue;
    public int rewardAmount;
}
