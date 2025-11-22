using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrencyUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreMultiplier;
    public TextMeshProUGUI highScore;

    public int level = 1;
    private int expThreshold = 100;

    private int lastCoins = -1;
    private int lastGems = -1;
    private int lastLevel = -1;
    private float lastMultiplier = -1f;
    private int lastHighScore = -1;

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

        if (HasChanged())
        {
            UpdateUI();
            CacheValues();
        }
    }

    private bool HasChanged()
    {
        return CurrencyManager.Instance.Coins != lastCoins ||
               CurrencyManager.Instance.Gems != lastGems ||
               level != lastLevel ||
               CurrencyManager.Instance.scoreMultiplier != lastMultiplier ||
               ScoreManager.Instance.highScore != lastHighScore;
    }

    private void CacheValues()
    {
        lastCoins = CurrencyManager.Instance.Coins;
        lastGems = CurrencyManager.Instance.Gems;
        lastLevel = level;
        lastMultiplier = CurrencyManager.Instance.scoreMultiplier;
        lastHighScore = ScoreManager.Instance.highScore;
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
