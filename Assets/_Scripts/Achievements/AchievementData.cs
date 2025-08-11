using UnityEngine;

public enum AchievementType
{
    CollectCoins,
    RunDistance,
    PickupItem,
    PlayCount,
    WatchAd,
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
