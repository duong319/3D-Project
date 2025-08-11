using System.Threading;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int Coins { get; private set; }
    public int Gems { get; private set; }

    public int Exp { get; private set; }
    public int HeadStart { get; private set; }
    public int ScoreBooster { get; private set; }
    public int scoreMultiplier;
    public int PlayerLevel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        Coins = PlayerPrefs.GetInt("Coins", 0);
        Gems = PlayerPrefs.GetInt("Gems", 0);
        Exp = PlayerPrefs.GetInt("Exp", 0);
        HeadStart = PlayerPrefs.GetInt("HeadStart", 0);
        ScoreBooster = PlayerPrefs.GetInt("ScoreBooster", 0);
        scoreMultiplier = PlayerPrefs.GetInt("scoreMultiplier", 1);
        PlayerLevel = FindFirstObjectByType<CurrencyUIManager>().level;
        AddscoreMultiplier(0);
        Debug.Log(Exp);
       // ResetProgress();
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("Coins", Coins);
    }

    public void SpendCoins(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt("Coins", Coins);
    }


    public void AddGems(int amount)
    {
        Gems += amount;
        PlayerPrefs.SetInt("Gems", Gems);
    }

    public void SpendGems(int amount)
    {
        Gems -= amount;
        PlayerPrefs.SetInt("Gems", Gems);
    }

    public void AddExp(int amount)
    {
        Exp += amount;
        PlayerPrefs.SetInt("Exp", Exp);
        
    }

    public void AddScoreBooster(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("ScoreBooster", ScoreBooster);
    }

    public void SpendScoreBooster(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt("ScoreBooster", ScoreBooster);
    }

    public void AddHeadStart(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("HeadStart", HeadStart);
    }

    public void SpendHeadStart(int amount)
    {
        Coins -= amount;
        PlayerPrefs.SetInt("HeadStart", HeadStart);
    }

    public void AddscoreMultiplier(int amount)
    {
        scoreMultiplier += amount;
        PlayerPrefs.SetInt("scoreMultiplier", scoreMultiplier);
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
