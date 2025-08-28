using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text scoreText;
    public Text coinText;
    public Text ScoreMultiplerText;
    public Text HeadStart;
    public Text ScoreBooster;

    public Text MissionScoreMultipler;
    public GameObject PausePanel;
    public CoundownText countdownText;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        ScoreMultiplerText.text = CurrencyManager.Instance.scoreMultiplier.ToString();
        HeadStart.text = CurrencyManager.Instance.HeadStart.ToString();
        ScoreBooster.text = CurrencyManager.Instance.ScoreBooster.ToString();
        MissionScoreMultipler.text = CurrencyManager.Instance.scoreMultiplier.ToString();
        countdownText = GetComponent<CoundownText>();
    }


    public void UpdateScoreMultiplier()
    {
        ScoreMultiplerText.text = CurrencyManager.Instance.scoreMultiplier.ToString();

    }
    public void UpdateScore(int value)
    {
        scoreText.text = value.ToString("D6");
    }

    public void UpdateCoins(int value)
    {
        coinText.text = value.ToString();
    }

    public void Pause()
    {
        AudioManager.Instance.Play("Btn");
        Time.timeScale = 0f;
        PausePanel.gameObject.SetActive(true);

    }

    public void Resume()
    {
        AudioManager.Instance.Play("Btn");
        PausePanel.gameObject.SetActive(false);
        countdownText.StartCountdown();

    }

    public void MainMenu()
    {
        AudioManager.Instance.Play("Close");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }


}
