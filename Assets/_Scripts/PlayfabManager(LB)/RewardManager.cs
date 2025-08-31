using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RewardData
{
    public int rank;
    public Sprite rewardIcon;
    public int coin;
}

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;

    public List<RewardData> rewards = new List<RewardData>();
    private Dictionary<int, RewardData> rewardDict;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        rewardDict = new Dictionary<int, RewardData>();
        foreach (var r in rewards)
        {
            if (!rewardDict.ContainsKey(r.rank))
                rewardDict.Add(r.rank, r);
        }
    }

    public RewardData GetReward(int rank)
    {
        if (rewardDict.ContainsKey(rank) && rank <= 3)
            return rewardDict[rank];
        return rewardDict[4];
    }

    public void GiveReward(int rank)
    {
        RewardData r = GetReward(rank);
        if (r != null)
        {
            Debug.Log($"Reward given! Rank {rank}: {r.coin} coin");
            CurrencyManager.Instance.AddCoins(r.coin);
        }
    }
}
