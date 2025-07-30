using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrencyUIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreMultiplierText;
    public Text scoreMultiplier;

    private int level = 1;
    private int expThreshold = 50;

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
        scoreMultiplierText.text = CurrencyManager.Instance.scoreMultiplier.ToString();
    }

    void LoadLevel()
    {
        level = PlayerPrefs.GetInt("PlayerLevel", 1);
    }
}
