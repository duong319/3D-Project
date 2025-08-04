using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Game/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public SpecialItemType itemType;
    public string upgradeName;
    public string description;
    public Sprite icon;
    public int[] prices;
    public float[] durations;
    public int maxLevel => prices.Length;
}

[System.Serializable]
public class UpgradeItem
{
    public UpgradeData data;
    public int level = 0;

    public bool CanUpgrade => level < data.maxLevel;

    public int CurrentPrice => CanUpgrade ? data.prices[level] : 0;
    public float CurrentDuration => level > 0 ? data.durations[level - 1] : 0f;

    public void Upgrade()
    {
        if (CanUpgrade)
            level++;
    }
}
public enum SpecialItemType
{
    Shield,
    Magnet,
    Headstart,
    ScoreMultiplier,
    ScoreBooster
}

