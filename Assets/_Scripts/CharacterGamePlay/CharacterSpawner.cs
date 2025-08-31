using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] outfits;

    private void Start()
    {
        string charKey = PlayerPrefs.GetString("SpawnKey", "G.Leslie");
        string outfitKey = PlayerPrefs.GetString("SelectedCharacterOutfit", "");

        foreach (var c in characters) c.SetActive(false);

        for (int i = 0; i < characters.Length; i++)
        {
            Debug.Log(charKey);
            if (characters[i].name == charKey)
            {
                characters[i].SetActive(true);

                if (!string.IsNullOrEmpty(outfitKey))
                {
                    foreach (Transform child in characters[i].transform)
                    {
                        if (child.name == outfitKey)
                        {
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                break;
            }
        }
    }
}