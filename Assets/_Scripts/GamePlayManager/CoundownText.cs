using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoundownText : MonoBehaviour
{
    public GameObject CountdownPanel;
    public float countdownTime = 3f;
    public Text countdownText;

    private float currentTime;
    private Coroutine countdownCoroutine;

    public void StartCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }

        countdownCoroutine = StartCoroutine(Countdown());
    }

    public void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
 
        CountdownPanel.SetActive(false);
    }

    IEnumerator Countdown()
    {
        Time.timeScale = 0f;
        CountdownPanel.SetActive(true);
        currentTime = countdownTime;
        while (currentTime >= 0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();
            yield return new WaitForSecondsRealtime(1f);
            currentTime--;
        }

        CountdownPanel.SetActive(false);
        Time.timeScale = 1f;
        countdownCoroutine = null;
    }
}