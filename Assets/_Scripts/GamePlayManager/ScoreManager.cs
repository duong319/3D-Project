using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

   
    public int currentCoins = 0;
    public Transform player;
    private float startZ;
    private int lastScore = 0;
    public int totalScore;

    public int scoreMultiplier;

    private void Awake()
    {
        if (Instance == null) Instance = this;
     
        AddscoreMultiplier(0);
        scoreMultiplier = PlayerPrefs.GetInt("scoreMultiplier");
        lastScore = PlayerPrefs.GetInt("highScore");

    }

    void Start()
    {
        startZ = player.position.z;
    }

    void Update()
    {
        UpdateScoreByDistance();
    }

    public void AddscoreMultiplier(int amount)
    {
        scoreMultiplier += amount;
        PlayerPrefs.SetInt("scoreMultiplier", 1);
    }

    void UpdateScoreByDistance()
    {
        float distanceZ = player.position.z - startZ;
       
        int calculatedScore = Mathf.FloorToInt(distanceZ * scoreMultiplier*0.05f);

        if (calculatedScore > lastScore)
        {
            lastScore = calculatedScore;
            UIManager.Instance.UpdateScore(lastScore);
            MissionManager.Instance.ReportProgress(MissionType.Score, calculatedScore);
            DailyScoreManager.Instance.UpdateHighScore(lastScore);
            PlayerPrefs.SetInt("highScore", 0);

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
}
