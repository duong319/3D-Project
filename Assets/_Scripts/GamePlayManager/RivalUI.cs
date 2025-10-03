using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RivalUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rivalNameText;
    [SerializeField] private TextMeshProUGUI remainHighScoreText;
    [SerializeField] private Text revivePanelText;
    private int rivalScore = -1;
    private void Start()
    {
        PlayFabLogin.Instance.GetLeaderboardAroundPlayer(OnLeaderboardAroundPlayer);
    }

    private void OnLeaderboardAroundPlayer(List<PlayerLeaderboardEntry> entries)
    {
        int myIndex = entries.FindIndex(e => e.PlayFabId == PlayFabLogin.Instance.playerId);

        if (myIndex > 0)
        {
            var rival = entries[myIndex - 1];
            string rivalName = string.IsNullOrEmpty(rival.DisplayName) ? "Unknown" : rival.DisplayName;

            rivalScore = rival.StatValue;
            rivalNameText.text = rivalName;
        }
        else
        {
            var me = entries[myIndex];
            string myName = me.DisplayName;
            rivalNameText.text = myName;
            rivalScore = -1;
            remainHighScoreText.text = "GGEZ!";
        }
    }
    private void Update()
    {
        if (rivalScore >= 0)
        {
            int remain = Mathf.Max(0, rivalScore - ScoreManager.Instance.highScore);
            remainHighScoreText.text = remain.ToString();
            revivePanelText.text = $"Need {remain} more to defeat {rivalNameText.text}!";
        }
        else
        {
            remainHighScoreText.text = "GGEZ!";
            revivePanelText.text = "1ST Place";
        }
    }
}
