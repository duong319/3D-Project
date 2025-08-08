using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite icon;
    public Sprite skinIcons;
    public GameObject characterPrefab;
    public int price;
    public int outfitPrice;
    public int unlockLevel;
    public Material outfitMaterial;
}
