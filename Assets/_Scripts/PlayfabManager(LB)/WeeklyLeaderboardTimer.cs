using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class WeeklyLeaderboardTimer : MonoBehaviour
{
    public Text timerText;
    public DayOfWeek resetDay = DayOfWeek.Monday;
    public int resetHourUTC = 0;

    private bool resetTriggered = false;
    private DateTime nextReset;
    private void Start()
    {
        nextReset = GetNextResetTime(DateTime.UtcNow);
        InvokeRepeating(nameof(UpdateTimer), 0f, 1f);
    }

    private void UpdateTimer()
    {
        DateTime now = DateTime.UtcNow;

        DateTime nextReset = GetNextResetTime(now);

        TimeSpan remaining = nextReset - now;

        if (remaining.TotalSeconds <= 0 && !resetTriggered)
        {
            resetTriggered = true;
            OnWeeklyReset();
            nextReset = GetNextResetTime(now.AddSeconds(1));
            resetTriggered = false;
        }

        if (remaining.TotalSeconds < 0) remaining = TimeSpan.Zero;

        timerText.text = string.Format("{0:D1}d{1:D2}h",
            remaining.Days,
            remaining.Hours);

    }

    private DateTime GetNextResetTime(DateTime currentTime)
    {

        int daysUntilReset = ((int)resetDay - (int)currentTime.DayOfWeek + 7) % 7;


        DateTime nextReset = currentTime.Date.AddDays(daysUntilReset).AddHours(resetHourUTC);
        if (nextReset <= currentTime)
        {
            nextReset = nextReset.AddDays(7);
        }

        return nextReset;
    }

    private void OnWeeklyReset()
    {
        PlayfabLeaderboard.Instance.GetPlayerRank((rank) =>
        {
            if (rank > 0)
            {
                RewardManager.Instance.GiveReward(rank);

            }
            ScoreManager.Instance.ResetHighScore();
        });
    }

}
