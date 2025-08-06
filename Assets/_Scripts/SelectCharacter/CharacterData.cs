using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite characterIcon;
    public GameObject characterPrefab;

    public UnlockType unlockType;
    public int unlockValue; 

    public int requiredLevelForPurchase;

    public List<OutfitData> outfits;
}

public enum UnlockType
{
    Free,
    LevelRequirement,
    PurchaseWithCoin,
}

[System.Serializable]
public class OutfitData
{
    public string outfitName;
    public Sprite icon;
    public int gemCost;
    public int requiredLevel;
    public bool unlocked; 
}
