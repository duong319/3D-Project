using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DailyScoreUI : MonoBehaviour
{
    public Slider ProgressBar;
    public Text highScoreText;
    public List<RewardTierUI> rewardTierUIs;
    public float totalProgress = 50000f;
    public Text resetTimerText;

    private void OnEnable()
    {
        UpdateUI();
        
    }

    public void UpdateUI()
    {
        ProgressBar.value = Mathf.Clamp01((float)DailyScoreManager.Instance.todayHighScore / totalProgress);
       
        int currentScore = DailyScoreManager.Instance.todayHighScore;
        highScoreText.text = currentScore.ToString();



        foreach (var tierUI in rewardTierUIs)
        {
            tierUI.UpdateTier(currentScore);
        }

        if (DailyScoreManager.Instance == null) return;

        var timeLeft = DailyScoreManager.Instance.GetTimeUntilReset();
        resetTimerText.text = string.Format("{0:D}h{1:D2}m",
            timeLeft.Hours, timeLeft.Minutes);
    }
}
