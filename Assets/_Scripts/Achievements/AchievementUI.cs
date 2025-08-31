using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementUI : MonoBehaviour
{
    public AchievementData data;

    public Image icon;
    public Image Icon;
    public Text titleText;
    public Text descText;
    public Slider progressBar;
    public Slider ProgressBar;
    public Text progressText;
    public Text ProgressText;
    public Button claimButton;
    public Button DetailButton;
    public GameObject DetailPanel;
    public Button closeBtn;
    public Text RewardAmount;
    public GameObject ClaimPanel;

    private void Start()
    {
        UpdateUI();
        claimButton.onClick.AddListener(Claim);
        DetailButton.onClick.AddListener(Detail);
        closeBtn.onClick.AddListener(Close);
    }

    public void SetData(AchievementData achievementData)
    {
        data = achievementData;
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (data == null) return;

        icon.sprite = data.icon;
        Icon.sprite = data.icon;
        titleText.text = data.title;
        descText.text = data.description;
        RewardAmount.text = data.rewardAmount.ToString();

        int current = AchievementManager.Instance.GetProgress(data.id);
        progressBar.maxValue = data.targetValue;
        progressBar.value = current;
        progressText.text = $"{current}/{data.targetValue}";
        ProgressBar.maxValue = data.targetValue;
        ProgressBar.value = current;
        ProgressText.text = $"{current}/{data.targetValue}";

        bool completed = current >= data.targetValue;
        bool claimed = AchievementManager.Instance.IsClaimed(data.id);

        claimButton.gameObject.SetActive(completed && !claimed);
        DetailButton.gameObject.SetActive(!completed);
    }

    private void Claim()
    {
        AchievementManager.Instance.ClaimReward(data);
        UpdateUI();
        AudioManager.Instance.Play("Claim");
        StartCoroutine(Claimpanel());
    }

    private void Detail()
    {
        AudioManager.Instance.Play("Btn");
        DetailPanel.gameObject.SetActive(true);
    }

    private void Close()
    {
        AudioManager.Instance.Play("Close");
        DetailPanel.gameObject.SetActive(false);
    }

    private IEnumerator Claimpanel()
    {
        ClaimPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        ClaimPanel.gameObject.SetActive(false);
    }
}
