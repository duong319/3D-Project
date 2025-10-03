using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemainScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainScoreText;

    private void Update()
    {
        UpdateRemainScore();
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
