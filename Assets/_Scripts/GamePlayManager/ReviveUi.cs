using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;


public class ReviveUi : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI countdownText;
    public float countdownTime = 5f;

    private float currentTime;
    private Coroutine countdownCoroutine;


    IEnumerator Countdown()
    {
        AudioManager.Instance.Play("SaveMe");
        while (currentTime >= 0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }   
        HidePanel();
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
        panel.SetActive(false);
        currentTime = 0f;
        AudioManager.Instance.Stop("SaveMe");
        AudioManager.Instance.Play("GamePlayBG");
        SceneManager.LoadScene("PlayerDead");
    }

    public void OnWatchAdClicked()
    {
        AudioManager.Instance.Stop("SaveMe");
        Debug.Log("Watch Ad Clicked");
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
