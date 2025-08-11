using UnityEngine;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    public List<AchievementData> achievements;
    private Dictionary<string, int> progress = new Dictionary<string, int>();
    private HashSet<string> claimed = new HashSet<string>();

    public event Action<AchievementData> OnAchievementCompleted;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        LoadData();
    }

    public void AddProgress(AchievementType type, int amount)
    {
        foreach (var a in achievements)
        {
            if (a.type == type && !claimed.Contains(a.id))
            {
                if (!progress.ContainsKey(a.id)) progress[a.id] = 0;

                progress[a.id] += amount;

                if (progress[a.id] >= a.targetValue)
                {
                    progress[a.id] = a.targetValue;
                    OnAchievementCompleted?.Invoke(a);
                }
            }
        }
        SaveData();
    }

    public int GetProgress(string id)
    {
        return progress.ContainsKey(id) ? progress[id] : 0;
    }

    public bool IsClaimed(string id) => claimed.Contains(id);

    public void ClaimReward(AchievementData a)
    {
        if (!claimed.Contains(a.id) && GetProgress(a.id) >= a.targetValue)
        {
            claimed.Add(a.id);
           
            Debug.Log($"Claimed {a.rewardAmount} coins from {a.title}");
            SaveData();
        }
    }

    private void SaveData()
    {
        foreach (var kvp in progress)
            PlayerPrefs.SetInt($"Ach_Prog_{kvp.Key}", kvp.Value);

        foreach (var id in claimed)
            PlayerPrefs.SetInt($"Ach_Claim_{id}", 1);
    }

    private void LoadData()
    {
        foreach (var a in achievements)
        {
            progress[a.id] = PlayerPrefs.GetInt($"Ach_Prog_{a.id}", 0);
            if (PlayerPrefs.GetInt($"Ach_Claim_{a.id}", 0) == 1)
                claimed.Add(a.id);
        }
    }
}
