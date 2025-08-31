
using UnityEngine;

public class CharacterSpawnerWithAnim : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] outfits;
    public Animator animator;

    private void Start()
    {
        string charKey = PlayerPrefs.GetString("SpawnKey", "G.Leslie");
        string outfitKey = PlayerPrefs.GetString("SelectedCharacterOutfit", "");
        animator = GetComponent<Animator>();

        foreach (var c in characters) c.SetActive(false);

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].name == charKey)
            {
                characters[i].SetActive(true);
                animator.SetBool(charKey, true);

                if (!string.IsNullOrEmpty(outfitKey))
                {
                    Debug.Log(charKey);
                    Debug.Log(outfitKey);
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
