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
    public Button SelectOutfitBtn;
    public Button showNormalBtn;
    public Button showOutfitButton;
    public Button viewOutfitBtn;
    public Text priceText;
    public Text SelectText;
    public Text SelectOutfitText;
    public Text OutfitPriceText;
    public Text ViewOutfitText;
    public GameObject lockedPanel;
    public GameObject lockedOutfitPanel;
    public GameObject SelectedMark;
    public GameObject SelectedOutfitMark;
    public Text unlockText;
    public Image SelectBtnImage;
    public Sprite selectSprite;
    public Sprite selectedSprite;
    public Image SelectOutfitBtnImage;


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


        bool unlocked = unlockManager.IsCharacterUnlocked(data);
        bool isowned = unlockManager.IsOwned(data);
        bool isSelected = unlockManager.IsSelected(data);
        bool isownedoutfit = unlockManager.IsOwnedOutfit(data);
        bool isSelectedOutfit = unlockManager.IsOutfitSelected(data);

        lockedPanel.SetActive(!unlocked);
        lockedOutfitPanel.SetActive(!isownedoutfit);
        showOutfitButton.onClick.RemoveAllListeners();
        showOutfitButton.onClick.AddListener(ShowOutfit);
        showNormalBtn.onClick.RemoveAllListeners();
        showNormalBtn.onClick.AddListener(PreviewCharacter);

        ViewProgressBtn.gameObject.SetActive(isShowOutfit);
        SelectOutfitBtn.gameObject.SetActive(isShowOutfit);
        SelectBtn.gameObject.SetActive(!isShowOutfit);
        viewOutfitBtn.gameObject.SetActive(isShowOutfit);
        SelectedMark.gameObject.SetActive(isSelected);
        SelectedOutfitMark.gameObject.SetActive(isSelectedOutfit);



        if (!isShowOutfit)
        {
            Debug.Log(isSelected);
            charName.text = data.characterName;
            ViewProgressBtn.gameObject.SetActive(!unlocked);
            SelectBtn.gameObject.SetActive(isowned);
            SelectText.text = isSelected ? "Selected" : "Select";
            SelectBtnImage.sprite = isSelected ? selectedSprite : selectSprite;



            purchaseOutfitButton.gameObject.SetActive(false);

            if (!unlocked)
            {
                ViewProgressBtn.onClick.RemoveAllListeners();
                ViewProgressBtn.onClick.AddListener(ViewProgress);
                unlockText.text = $"LV.{data.unlockLevel} UNLOCK";

            }
            else if (unlocked)
            {
                unlockText.text = $" ";
                purchaseButton.gameObject.SetActive(!isowned);

                if (isowned)
                {

                    SelectBtn.onClick.RemoveAllListeners();
                    SelectBtn.onClick.AddListener(SelectCharacter);

                }
                else if (!isowned)
                {

                    priceText.text = data.price.ToString();
                    purchaseButton.onClick.RemoveAllListeners();
                    purchaseButton.onClick.AddListener(BuyCharacter);
                }
            }

        }
        else if (isShowOutfit)
        {
            Debug.Log(isSelectedOutfit);
            charName.text = data.characterOutfitName;
            viewOutfitBtn.gameObject.SetActive(!unlocked);
            ViewProgressBtn.gameObject.SetActive(!unlocked);

            SelectOutfitBtn.gameObject.SetActive(isownedoutfit);
            SelectOutfitText.text = isSelectedOutfit ? "Selected" : "Select";
            SelectOutfitBtnImage.sprite = isSelectedOutfit ? selectedSprite : selectSprite;

            purchaseButton.gameObject.SetActive(false);


            if (!unlocked)
            {
                ViewOutfitText.text = $"Unlock {data.name} First ";
            }
            else if (unlocked)
            {

                purchaseOutfitButton.gameObject.SetActive(!isownedoutfit);
                if (!isownedoutfit)
                {
                    purchaseOutfitButton.onClick.RemoveAllListeners();
                    purchaseOutfitButton.onClick.AddListener(BuyCharacterOutfit);
                    OutfitPriceText.text = data.outfitPrice.ToString();
                }
                else if (isownedoutfit)
                {
                    SelectOutfitBtn.onClick.RemoveAllListeners();
                    SelectOutfitBtn.onClick.AddListener(SelectCharacterOutfit);
                }

            }
        }

    }

    void ShowOutfit()
    {


        if (characterData != null && previewManager != null)
        {
            isShowOutfit = true;
            previewManager.ShowOutfit(characterData);
            Debug.Log($"Show outfit: {characterData.characterName}");
            Init(characterData, unlockManager, previewManager);
            
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
        if (unlockManager.IsCharacterUnlocked(characterData) && unlockManager.IsOwned(characterData) && !unlockManager.IsOwnedOutfit(characterData))
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
        if (unlockManager.IsOwned(characterData) && !unlockManager.IsSelected(characterData))
        {
            unlockManager.SelectCharacter(characterData);
            Debug.Log($"Selected: {characterData.characterName}");
            Init(characterData, unlockManager, previewManager);
        }
    }

    void SelectCharacterOutfit()
    {
        if (unlockManager.IsOwnedOutfit(characterData) && !unlockManager.IsOutfitSelected(characterData))
        {
            unlockManager.SelectCharacterOutfit(characterData);
            Debug.Log($"Selected: {characterData.characterName} Outfit");
            Init(characterData, unlockManager, previewManager);
        }
    }

    void ViewProgress()
    {
        SceneManager.LoadScene("Level");
    }

    public void PreviewCharacter()
    {


        if (characterData != null && previewManager != null)
        {
            isShowOutfit = false;
            previewManager.ShowCharacter(characterData);
            Debug.Log(characterData.characterName);
            Init(characterData, unlockManager, previewManager);
            AudioManager.Instance.Play(characterData.characterName);
        }


    }


}
