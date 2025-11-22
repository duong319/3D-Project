using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReviveUi : MonoBehaviour
{
    public GameObject panel;
    public GameObject[] countdownImages;
    public float countdownTime = 6f;

    private float currentTime;
    private Coroutine countdownCoroutine;


    IEnumerator Countdown()
    {
        AudioManager.Instance.Play("SaveMe");
        while (currentTime > 0)
        {
            UpdateCountdownImages(Mathf.CeilToInt(currentTime));
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        HidePanel();
    }

    private void UpdateCountdownImages(int timeLeft)
    {
        foreach (var img in countdownImages)
        {
            img.SetActive(false);
        }

        int index = Mathf.Clamp(countdownImages.Length - timeLeft, 0, countdownImages.Length - 1);
        if (timeLeft > 0 && index < countdownImages.Length)
        {
            countdownImages[index].SetActive(true);
        }
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        currentTime = countdownTime;
        countdownCoroutine = StartCoroutine(Countdown());
    }

    public void HidePanel()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        foreach (var img in countdownImages)
        {
            img.SetActive(false);
        }
        panel.SetActive(false);
        currentTime = 0f;
        AudioManager.Instance.Stop("SaveMe");
        AudioManager.Instance.Play("GamePlayBG");
        if (SpecialItemManager.Instance.ScoreBoosterEnabled == true)
        {
            SpecialItemManager.Instance.EndScoreBooster();
        }
        SceneManager.LoadScene("PlayerDead");
    }

    public void OnWatchAdClicked()
    {
        AudioManager.Instance.Stop("SaveMe");      
        RewardedAdsButton.Instance.LoadAd(Rewardtype.None);

        RewardedAdsButton.Instance.onAdCompleted = () =>
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
            }
            PlayerController.Instance.Revive();
            panel.SetActive(false);
            AudioManager.Instance.Play("GamePlayBG");
        };

    }

    public void OnCloseClicked()
    {
        AudioManager.Instance.Play("Btn");
        HidePanel();
    }
}
