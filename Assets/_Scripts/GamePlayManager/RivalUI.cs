using UnityEngine;

using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine.UI;

public class RivalUI : MonoBehaviour
{
    [SerializeField] private Text rivalNameText;
    [SerializeField] private Text remainHighScoreText;

    

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
            int rivalScore = rival.StatValue;

            rivalNameText.text = rivalName;

            int remain = Mathf.Max(0, rivalScore - ScoreManager.Instance.highScore);
            remainHighScoreText.text = $"Need {remain} more!";
        }
        else
        {
            var me = entries[myIndex];
            string myName =  me.DisplayName;
            rivalNameText.text = myName;
            remainHighScoreText.text = "GGEZ!";
        }
    }
}
