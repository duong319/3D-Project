
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
    public int AchievementMedals;
    public int totalExp { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        LoadCurrencies();
        // ResetProgress();
    }

    public void LoadCurrencies()
    {
        FindFirstObjectByType<CurrencyUIManager>().LoadLevel();
        PlayerLevel = FindFirstObjectByType<CurrencyUIManager>().level;
        Coins = PlayerPrefs.GetInt("Coins", 0);
        Gems = PlayerPrefs.GetInt("Gems", 0);
        Exp = PlayerPrefs.GetInt("Exp", 0);
        totalExp = PlayerPrefs.GetInt("totalExp", 0);
        HeadStart = PlayerPrefs.GetInt("HeadStart", 0);
        ScoreBooster = PlayerPrefs.GetInt("ScoreBooster", 0);
        scoreMultiplier = PlayerPrefs.GetInt("scoreMultiplier", 1);
        AchievementMedals = PlayerPrefs.GetInt("Medal", 0);
        AddscoreMultiplier(0);
    }

    public void AddMedal(int amount)
    {
        AchievementMedals += amount;
        PlayerPrefs.SetInt("Medal", AchievementMedals);
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
        AchievementManager.Instance.AddProgress(AchievementType.SpendGem, amount);
    }

    public void AddExp(int amount)
    {
        Exp += amount;
        PlayerPrefs.SetInt("Exp", Exp);
    }
    public void AddTotalExp(int amount)
    {
        totalExp += amount;
        FindFirstObjectByType<CurrencyUIManager>().LoadLevel();
        PlayerLevel = FindFirstObjectByType<CurrencyUIManager>().level;
        PlayerPrefs.SetInt("totalExp", totalExp);
    }

    public void AddScoreBooster(int amount)
    {
        ScoreBooster += amount;
        PlayerPrefs.SetInt("ScoreBooster", ScoreBooster);
    }

    public void SpendScoreBooster(int amount)
    {
        ScoreBooster -= amount;
        PlayerPrefs.SetInt("ScoreBooster", ScoreBooster);
    }

    public void AddHeadStart(int amount)
    {
        HeadStart += amount;
        PlayerPrefs.SetInt("HeadStart", HeadStart);
    }

    public void SpendHeadStart(int amount)
    {
        HeadStart -= amount;
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
        PlayerPrefs.Save();
    }
}
