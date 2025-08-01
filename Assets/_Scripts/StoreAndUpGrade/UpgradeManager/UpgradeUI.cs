using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public Image iconImage;
    public Text nameText;
    public Text descText;
    public Text priceText;
    public Text durationText;
    public Button upgradeButton;
    public Sprite activateProgress;
    public Sprite Progress;
    public RectTransform durationTxt;
    public Transform[] levelbars;


    public Image[] levelBars;
    private UpgradeItem upgradeItem;

    public void Setup(UpgradeItem item, int index, System.Action<int> onUpgradeClick)
    {
        upgradeItem = item;

        iconImage.sprite = item.data.icon;
        nameText.text = item.data.upgradeName.ToUpper();
        descText.text = item.data.description;
        durationText.text = $"{item.CurrentDuration}s";

        UpdateLevelBars(item.level, item.data.maxLevel);

        if (item.CanUpgrade)
        {
            priceText.text = item.CurrentPrice.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            priceText.text = "MAX";
            upgradeButton.interactable = false;
        }

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(() => onUpgradeClick(index));
    }

    private void UpdateLevelBars(int currentLevel, int maxLevel)
    {
        for (int i = 0; i < levelBars.Length; i++)
        {
            levelBars[i].enabled = i < maxLevel;
            levelBars[i].sprite = (i < currentLevel) ? activateProgress : Progress;

        }
        if (currentLevel <= 0 || currentLevel > levelBars.Length)
            return;

       
        durationTxt.SetParent(levelbars[currentLevel - 1], false); 

        
        durationTxt.anchoredPosition = Vector2.zero;


    }
}
