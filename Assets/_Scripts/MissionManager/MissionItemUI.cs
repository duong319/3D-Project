
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
    public Button ClaimBtn;


    private Mission mission;

    public void Setup(Mission mission)
    {
        this.mission = mission;

        descriptionText.text = mission.data.description;
        progressSlider.maxValue = mission.data.targetAmount;
        skipCostText.text = $"{mission.data.skipCost}";
        ClaimedDescriptionText.text = descriptionText.text;
        ClaimBtn.interactable = mission.isCompleted;
        UpdateProgress();
       
    }

    public void UpdateProgress()
    {
        if (mission == null) return;
       
        skipButton.interactable = true;
        skipButton.onClick.AddListener(OnSkip);
    

        if (mission.isCompleted) OnCompleted();
        progressSlider.value = mission.currentAmount;
        progressText.text = $"{mission.currentAmount}/{mission.data.targetAmount}";
        
        ClaimBtn.onClick.AddListener(OnClaim);

    }

    void OnCompleted()
    {
        MissionClaimed.gameObject.SetActive(true);
        progressSlide.SetActive(false);
        
    }
    
    public void OnClaim()
    {
        AudioManager.Instance.Play("Claim");
        MissionManager.Instance.ClaimReward(mission);
        Debug.Log("claim");
    }

   public void OnSkip()
    {
        AudioManager.Instance.Play("Btn");
        if (CurrencyManager.Instance.Coins < mission.data.skipCost) return;
        MissionManager.Instance.SkipMission(mission);
        progressSlide.SetActive(false);
        MissionClaimed.gameObject.SetActive(true);
        Debug.Log("skip");
    }
}
