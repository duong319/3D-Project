using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemUI : MonoBehaviour
{
    public Image icon;
    public Text amountText;
    public Text nameText;

    public void Setup(Sprite rewardIcon, string rewardName, int amount)
    {
        icon.sprite = rewardIcon;
        nameText.text = rewardName;
        amountText.text = "" + amount;
    }
}
