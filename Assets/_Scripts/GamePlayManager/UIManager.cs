using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI ScoreMultiplerText;
    public TextMeshProUGUI HeadStart;
    public TextMeshProUGUI ScoreBooster;
    public TextMeshProUGUI MissionScoreMultipler;
    public GameObject PausePanel;
    public CoundownText countdownText;
    public GameObject LeavePanel;

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

    public void OpenLeavePanel()
    {
        LeavePanel.gameObject.SetActive(true);
    }

    public void CloseLeavePanel()
    {
        LeavePanel.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        AudioManager.Instance.Play("Close");
        AudioManager.Instance.Play("MenuBG");
        AudioManager.Instance.Stop("GamePlayBG");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
