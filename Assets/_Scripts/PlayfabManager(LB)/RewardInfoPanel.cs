using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button bgButton;

    private void Awake()
    {
        if (bgButton == null)
            bgButton = FindFirstObjectByType<BackgroundButton>()?.GetComponent<Button>();   
    }

    private void Start()
    {
        bgButton.onClick.AddListener(HideReward);
    }
    public void ShowReward(RewardData reward)
    {
        rewardText.text = $"+{reward.coin.ToString()}";
        panel.SetActive(true);
    }

    private void HideReward()
    {
        panel.SetActive(false);     
    }


}
