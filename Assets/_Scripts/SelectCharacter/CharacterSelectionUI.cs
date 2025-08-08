using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public CharacterDatabase database;
    public CharacterUnlockManager unlockManager;
    public UICharacterSlot slotPrefab;
    public Transform contentPanel;
    public CharacterPreviewManager previewManager;

    void Start()
    {
      
        if (database.characters.Count > 0)
        {
            ShowCharacterByIndex(0);
        }
        
    }

    void PopulateCharacter(CharacterData character)
    {
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        var slot = Instantiate(slotPrefab, contentPanel);
        slot.Init(character, unlockManager, previewManager);
        slot.PreviewCharacter();
    }

    public void ShowCharacterByIndex(int index)
    {
        if (index >= 0 && index < database.characters.Count)
        {
            PopulateCharacter(database.characters[index]);
            Debug.Log(index);
        }
    }

    public void ShowCharacterByName(string characterName)
    {
        var character = database.characters.Find(c => c.characterName == characterName);
        if (character != null)
        {
            PopulateCharacter(character);
        }
    }
}
