using UnityEngine;
using static DailyScoreRewardData;

[CreateAssetMenu(menuName = "SpecialItem")]
public class SpecialItem : ScriptableObject
{
    public SpecialItemType itemType;
    public string itemName;
    public Sprite icon;
    public int duration;
}



