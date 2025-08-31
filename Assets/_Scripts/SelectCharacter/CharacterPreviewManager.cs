using UnityEngine;

public class CharacterPreviewManager : MonoBehaviour
{
    public Transform previewPosition;
    private GameObject currentPreview;
    private SkinnedMeshRenderer characterRenderer;

    public void ShowCharacter(CharacterData data)
    {
        if (currentPreview != null)
            Destroy(currentPreview);

        currentPreview = Instantiate(data.characterPrefab, previewPosition.position, Quaternion.identity);
        currentPreview.transform.SetParent(previewPosition, false);
        characterRenderer = currentPreview.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void ShowOutfit(CharacterData data)
    {
        if (characterRenderer != null && data != null && data.outfitMaterial != null)
        {
            characterRenderer.material = data.outfitMaterial;
        }
    }

}
