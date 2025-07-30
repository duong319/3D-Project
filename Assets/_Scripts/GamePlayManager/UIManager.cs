using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text scoreText;
    public Text coinText;
    public Text ScoreMultiplerText;
    public TextMeshProUGUI scoreMultiplerTxt;
    public Text MissionScoreMultipler;
    public GameObject PausePanel;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        ScoreMultiplerText.text = CurrencyManager.Instance.scoreMultiplier.ToString();
        scoreMultiplerTxt.text = CurrencyManager.Instance.scoreMultiplier.ToString();
        MissionScoreMultipler.text= CurrencyManager.Instance.scoreMultiplier.ToString();
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
        Time.timeScale = 0f;
        PausePanel.gameObject.SetActive(true);
          
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.gameObject.SetActive(false);
       
    }

    public void Setting()
    {

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");  
    }


}
