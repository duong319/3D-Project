using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionItemUI : MonoBehaviour
{
    public Text descriptionText;
    public Text progressText;
    public Text ClaimedDescriptionText;
    public Slider progressSlider;
    public Button skipButton;
    public GameObject MissionClaimed;
    public Text skipCostText;
    public GameObject progressSlide;

    private Mission mission;

    public void Setup(Mission mission)
    {
        this.mission = mission;

        descriptionText.text = mission.data.description;
        progressSlider.maxValue = mission.data.targetAmount;
        skipCostText.text = $"{mission.data.skipCost}";
        ClaimedDescriptionText.text = descriptionText.text;
        UpdateProgress();
       
    }

    public void UpdateProgress()
    {
        if (mission == null) return;
        skipButton.onClick.AddListener(OnSkip);
        if (mission.isCompleted) OnClaim();
        progressSlider.value = mission.currentAmount;
        progressText.text = $"{mission.currentAmount}/{mission.data.targetAmount}";
    }

    void OnClaim()
    {
        MissionClaimed.gameObject.SetActive(true);
        progressSlide.SetActive(false);

        MissionManager.Instance.ClaimReward(mission);
       
    }

    void OnSkip()
    {
        MissionManager.Instance.SkipMission(mission);
        progressSlide.SetActive(false);
        MissionClaimed.gameObject.SetActive(true);
    }
}
