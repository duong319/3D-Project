using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{
    public CharacterDatabase database;
    public CharacterUnlockManager unlockManager;
    public UICharacterSlot slotPrefab;
    public Transform contentPanel;
    public CharacterPreviewManager previewManager;
    public GameObject[] SelectedMark;

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

            if (index == 0)
            {
                AudioManager.Instance.Play("G.Leslie");
                AudioManager.Instance.Stop("B.Hailey");
                AudioManager.Instance.Stop("R.Caitlin");
            }
            else if (index == 1)
            {
                AudioManager.Instance.Play("B.Hailey");
                AudioManager.Instance.Stop("G.Leslie");
                AudioManager.Instance.Stop("R.Caitlin");
            }
            else if (index == 2)
            {
                AudioManager.Instance.Play("R.Caitlin");
                AudioManager.Instance.Stop("G.Leslie");
                AudioManager.Instance.Stop("B.Hailey");
            }
            for (int i = 0; i <= SelectedMark.Length - 1; i++)
            {
                if (i == index)
                {
                    SelectedMark[i].gameObject.SetActive(true);
                }
                else
                {
                    SelectedMark[i].gameObject.SetActive(false);
                }
            }
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
