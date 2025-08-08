using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterUnlockManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    private const string SelectedKey = "SelectedCharacter";


    public bool IsCharacterUnlocked(CharacterData character)
    {
        if (character.price == 0) return true;
        return CurrencyManager.Instance.PlayerLevel >= character.unlockLevel;
    }

    public bool IsOwned(CharacterData data)
    {
        if (data.price == 0) return true;
        return PlayerPrefs.GetInt(GetOwnKey(data), 0) == 1;
    }

    public bool IsOwnedOutfit(CharacterData data)
    {
        return PlayerPrefs.GetInt(GetOwnOutfitKey(data), 0) == 1;
    }

    public void BuyCharacter(CharacterData data)
    {
        if (IsCharacterUnlocked(data))
        {
            PlayerPrefs.SetInt(GetOwnKey(data), 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("Locked!");
        }
    }

    public void BuyCharacterOutfit(CharacterData data)
    {
        if (IsCharacterUnlocked(data) && IsOwned(data))
        {
            PlayerPrefs.SetInt(GetOwnOutfitKey(data), 1);
            PlayerPrefs.Save();
        }

    }

    private string GetOwnKey(CharacterData data)
    {
        return $"CharacterOwned_{data.characterName}";
    }
    private string GetOwnOutfitKey(CharacterData data)
    {
        return $"CharacterOutfitOwned_{data.characterName}";
    }

    public void SelectCharacter(CharacterData data)
    {
        if (IsOwned(data))
        {
            PlayerPrefs.SetString(SelectedKey, data.characterName);
            PlayerPrefs.Save();
        }
    }

    public string GetSelectedCharacterName()
    {
        return PlayerPrefs.GetString(SelectedKey, "");
    }

    public bool IsSelected(CharacterData data)
    {
        if (data.price == 0) return true;
        return GetSelectedCharacterName() == data.characterName;
    }






    public void ResetAll()
    {
        foreach (var character in FindObjectOfType<CharacterDatabase>().characters)
        {
            PlayerPrefs.DeleteKey(GetOwnKey(character));
        }
        PlayerPrefs.Save();
    }
}
