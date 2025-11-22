
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankIcon
{
    public int rank;
    public Sprite rankSprite;
}

public class RankIconManager : MonoBehaviour
{
    public static RankIconManager Instance;
    public List<RankIcon> rankIcons = new List<RankIcon>();
    private Dictionary<int, Sprite> rankDict;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }

        rankDict = new Dictionary<int, Sprite>();
        foreach (var r in rankIcons)
        {
            if (!rankDict.ContainsKey(r.rank))
                rankDict.Add(r.rank, r.rankSprite);
        }
    }

    public Sprite GetRankSprite(int rank)
    {
        if (rankDict.ContainsKey(rank))
            return rankDict[rank];
        return null;
    }
}
