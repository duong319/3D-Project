using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UICharacterSlot : MonoBehaviour
{
    public Image charIcon;
    public Image charOutfit;
    public Text charName;
    public Button purchaseButton;
    public Button purchaseOutfitButton;
    public Button ViewProgressBtn;
    public Button SelectBtn;
    public Button showNormalBtn;
    public Button showOutfitButton;
    public Button viewOutfitBtn;
    public Text priceText;
    public Text SelectText;
    public Text OutfitPriceText;
    public Text ViewOutfitText;
    public GameObject lockedPanel;
    public GameObject lockedOutfitPanel;
    public Text unlockText;
    public Image SelectBtnImage;
    public Sprite selectSprite;
    public Sprite selectedSprite;

    private bool isShowOutfit = false;
    private CharacterData characterData;
    private CharacterUnlockManager unlockManager;
    private CharacterPreviewManager previewManager;




    public void Init(CharacterData data, CharacterUnlockManager manager, CharacterPreviewManager preview)
    {
        characterData = data;
        unlockManager = manager;
        previewManager = preview;

        charIcon.sprite = data.icon;
        charOutfit.sprite = data.skinIcons;
        charName.text = data.characterName;

        bool unlocked = unlockManager.IsCharacterUnlocked(data);
        bool isowned = unlockManager.IsOwned(data);
        bool isSelected = unlockManager.IsSelected(data);
        bool isownedoutfit=unlockManager.IsOwnedOutfit(data);




        if (unlocked && !isowned && !isownedoutfit)
        {
            if (isShowOutfit == false)
            {
                lockedPanel.SetActive(false);
                purchaseButton.gameObject.SetActive(true);
                priceText.text = data.price.ToString();
                purchaseButton.onClick.RemoveAllListeners();
                purchaseButton.onClick.AddListener(BuyCharacter);
            }
            purchaseButton.gameObject.SetActive(false);
            purchaseOutfitButton.gameObject.SetActive(true);
            OutfitPriceText.text = data.outfitPrice.ToString();
            purchaseOutfitButton.onClick.RemoveAllListeners();
            purchaseOutfitButton.onClick.AddListener(BuyCharacterOutfit);
        }



        else if (!unlocked )
        {
            if (isShowOutfit == false)
            {
                ViewProgressBtn.gameObject.SetActive(true);
                SelectBtn.gameObject.SetActive(false);
                unlockText.text = $"LV.{data.unlockLevel} UNLOCK";
                lockedPanel.SetActive(true);
                ViewProgressBtn.onClick.RemoveAllListeners();
                ViewProgressBtn.onClick.AddListener(ViewProgress);
            }
            ViewProgressBtn.gameObject.SetActive(false);
            SelectBtn.gameObject.SetActive(false);
            viewOutfitBtn.gameObject.SetActive(true);
            ViewOutfitText.text = $"Unlock {data.name} First ";

        }
        else
        {
            lockedPanel.SetActive(false);
            purchaseButton.gameObject.SetActive(false);
            ViewProgressBtn.gameObject.SetActive(false);
            SelectBtn.gameObject.SetActive(true);
            SelectText.text = isSelected ? "Selected" : "Select";
            SelectBtnImage.sprite = isSelected ? selectedSprite : selectSprite;
            unlockText.text = "";
            SelectBtn.onClick.RemoveAllListeners();
            SelectBtn.onClick.AddListener(SelectCharacter);
        }
        showOutfitButton.onClick.RemoveAllListeners();
        showOutfitButton.onClick.AddListener(ShowOutfit);
        showNormalBtn.onClick.RemoveAllListeners();
        showNormalBtn.onClick.AddListener(PreviewCharacter);


    }

    void ShowOutfit()
    {
        isShowOutfit = true;
        if (characterData != null && previewManager != null)
        {
            previewManager.ShowOutfit(characterData);
            Debug.Log($"Show outfit: {characterData.characterName}");

        }
    }

    void BuyCharacter()
    {
        if (unlockManager.IsCharacterUnlocked(characterData) && !unlockManager.IsOwned(characterData))
        {

            unlockManager.BuyCharacter(characterData);
            Init(characterData, unlockManager, previewManager);
            Debug.Log($"buy: {characterData.characterName}");
        }
        else
        {
            Debug.LogWarning("locked");
        }

    }
    void BuyCharacterOutfit()
    {
        if (unlockManager.IsCharacterUnlocked(characterData) && unlockManager.IsOwned(characterData)&&!unlockManager.IsOwnedOutfit(characterData))
        {

            unlockManager.BuyCharacterOutfit(characterData);
            Init(characterData, unlockManager, previewManager);
            Debug.Log($"buy: {characterData.characterName}");
        }
        else
        {
            Debug.LogWarning("locked");
        }

    }

    void SelectCharacter()
    {
        if (unlockManager.IsOwned(characterData))
        {
            unlockManager.SelectCharacter(characterData);
            Debug.Log($"Selected: {characterData.characterName}");
        }
    }

    void ViewProgress()
    {
        SceneManager.LoadScene("Level");
    }

    public void PreviewCharacter()
    {
        isShowOutfit = false;
        if (characterData != null)
        {     
            previewManager.ShowCharacter(characterData);
            Debug.Log(characterData.characterName);
        }
       

    }


}
