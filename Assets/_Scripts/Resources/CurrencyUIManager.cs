using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrencyUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI levelText;
    public Text scoreMultiplier;
    public TextMeshProUGUI highScore;

    public int level = 1;
    private int expThreshold = 100;

    private void Start()
    {
        LoadLevel();
        UpdateUI();
    }

    private void Update()
    {
        if (CurrencyManager.Instance.Exp >= expThreshold)
        {
            CurrencyManager.Instance.AddExp(-expThreshold);
            level++;
            PlayerPrefs.SetInt("PlayerLevel", level);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = CurrencyManager.Instance.Coins.ToString();
        gemText.text = CurrencyManager.Instance.Gems.ToString();
        levelText.text = "Level " + level.ToString();
        scoreMultiplier.text = CurrencyManager.Instance.scoreMultiplier.ToString();
        highScore.text = ScoreManager.Instance.highScore.ToString();
    }

    public void LoadLevel()
    {
        level = PlayerPrefs.GetInt("PlayerLevel", 1);
    }
}
