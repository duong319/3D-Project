using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterUnlockData", menuName = "Character/UnlockData")]
public class CharacterUnlockData : ScriptableObject
{
    public string characterId;
    public Sprite characterIcon;
    public GameObject characterPrefab;
    public int unlockLevel;
    public int unlockPrice;
    public List<SkinData> skins;
}

[System.Serializable]
public class SkinData
{
    public string skinId;
    public Sprite skinIcon;
    public int unlockPrice;
}
