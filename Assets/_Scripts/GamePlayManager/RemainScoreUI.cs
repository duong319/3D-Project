using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemainScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainScoreText;
    private float refreshRate = 0.1f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= refreshRate)
        {
            UpdateRemainScore();
            timer = 0;
        }
        
    }

    private void UpdateRemainScore()
    {
        var manager = DailyScoreManager.Instance;
        if (manager == null) return;
        int remain = manager.GetRemainingScore();
        var nextTier = manager.GetNextTier();

        if (nextTier == null)
        {
            remainScoreText.text = "Done!";
        }
        else if (remain > 0)
        {
            remainScoreText.text = remain.ToString();
        }
        else
        {
            remainScoreText.text = $"Ready!";
        }
    }
}
