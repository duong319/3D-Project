using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoundownText : MonoBehaviour
{
    public GameObject CountdownPanel;
    public float countdownTime = 3f;
    public Text countdownText;

    private float currentTime;

    public void StartCountdown()
    {
        StartCoroutine(Countdown());
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
    }
}