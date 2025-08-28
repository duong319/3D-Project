using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;


    public int currentCoins = 0;
    public Transform player;
    private float startZ;
    public int lastScore = 0;
    public int highScore;
    public int totalScore;
    private int baseScore = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }

    void Start()
    {
        startZ = player.position.z;
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }

    void Update()
    {
        UpdateScoreByDistance();    
        highScore = PlayerPrefs.GetInt("highScore", highScore);
        totalScore = PlayerPrefs.GetInt("totalScore", 0);
    }



    void UpdateScoreByDistance()
    {

        float distanceZ = player.position.z - startZ;

        int newBaseScore = Mathf.FloorToInt(distanceZ * 0.05f);

        if (newBaseScore > baseScore)
        {
            int gained = newBaseScore - baseScore;
            baseScore = newBaseScore;

            int gainedWithMultiplier = gained * CurrencyManager.Instance.scoreMultiplier;
            lastScore += gainedWithMultiplier;
            UIManager.Instance.UpdateScore(lastScore);
            MissionManager.Instance.ReportProgress(MissionType.Score, lastScore);
            DailyScoreManager.Instance.UpdateHighScore(lastScore);
            if (lastScore > highScore)
            {
                highScore = lastScore;
                PlayerPrefs.SetInt("highScore", highScore);
                PlayerPrefs.Save();
            }
            totalScore = PlayerPrefs.GetInt("totalScore", 0) + gainedWithMultiplier;
            PlayerPrefs.SetInt("totalScore", totalScore);
        }

    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        UIManager.Instance.UpdateCoins(currentCoins);
    }

    public void Reset()
    {
        startZ = player.position.z;
        lastScore = 0;
        currentCoins = 0;
        UIManager.Instance.UpdateScore(0);
        UIManager.Instance.UpdateCoins(0);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("highScore");
    }
}
