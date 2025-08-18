using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ReviveUi : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI countdownText;
    public float countdownTime = 5f;

    private float currentTime;

   

    IEnumerator Countdown()
    {
        AudioManager.Instance.Play("SaveMe");
        while (currentTime >=0)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        SceneManager.LoadScene("PlayerDead");

        HidePanel();
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        currentTime = countdownTime;
        StartCoroutine(Countdown());
        Debug.Log(currentTime);
        Debug.Log("show");

    }

    public void HidePanel()
    {
        panel.SetActive(false);
        currentTime = 0f;
        AudioManager.Instance.Stop("SaveMe");
        AudioManager.Instance.Play("GamePlayBG");
    }

    public void OnWatchAdClicked()
    {    
        Debug.Log("Watch Ad Clicked");
        HidePanel();
    }

    public void OnCloseClicked()
    {
        AudioManager.Instance.Play("Btn");
        HidePanel();      
    }
}
