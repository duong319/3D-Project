using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayfabLeaderboard : MonoBehaviour
{
    public static PlayfabLeaderboard Instance;
    public string leaderboardName = "High Score";
    public int maxResultsCount = 10;

    public LeaderboardRowUI rowPrefab;
    public Transform contentParent;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        SendScore(ScoreManager.Instance.highScore);
    }

    public void SendScore(int score)
    {
        Debug.Log("Send LB");
        if (!PlayFabLogin.Instance.isLoggedIn)
        {
            return;
        }

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate { StatisticName = leaderboardName, Value = score }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result =>
            {
                GetLeaderboard();
            },
            error => Debug.LogError("Update fail " + error.GenerateErrorReport())
        );
    }

    public void GetLeaderboard()
    {
        Debug.Log("get lb");
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = maxResultsCount
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardSuccess, OnLeaderboardError);
    }

    private void OnLeaderboardSuccess(GetLeaderboardResult result)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        int total = result.Leaderboard.Count;
        int done = 0;

        var entries = new LeaderboardRowUI[result.Leaderboard.Count];

        foreach (var entry in result.Leaderboard)
        {
            GetPlayerCountry(entry.PlayFabId, (countryCode) =>
            {
                var row = Instantiate(rowPrefab, contentParent);
                row.SetData(entry.Position + 1, entry.DisplayName, entry.StatValue, countryCode);

                entries[entry.Position] = row;

                done++;

                if (done == total)
                {
                    for (int i = 0; i < entries.Length; i++)
                    {
                        entries[i].transform.SetSiblingIndex(i);
                    }
                }
            });
        }
    }

    private void GetPlayerCountry(string playFabId, System.Action<string> callback)
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowLocations = true
            }
        }, (result) =>
        {
            var countryCode = result.PlayerProfile?.Locations?.FirstOrDefault()?.CountryCode?.ToString();

            callback?.Invoke(countryCode ?? "");
        }, (error) =>
        {

            callback?.Invoke("");
        });
    }

    private void OnLeaderboardError(PlayFabError error)
    {
        Debug.LogError("Cant Get LeaderBoard " + error.GenerateErrorReport());
    }
    public void GetPlayerRank(Action<int> onRankReceived)
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = leaderboardName,
            MaxResultsCount = 1
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request,
            result =>
            {
                if (result.Leaderboard != null && result.Leaderboard.Count > 0)
                {
                    int rank = result.Leaderboard[0].Position + 1;
                    onRankReceived?.Invoke(rank);
                }
                else
                {
                    onRankReceived?.Invoke(-1);
                }
            },
            error =>
            {
                onRankReceived?.Invoke(-1);
            });
    }
}
