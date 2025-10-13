using UnityEngine;

[CreateAssetMenu(fileName = "NewMission", menuName = "Mission/Mission Data")]
public class MissionData : ScriptableObject
{
    public string missionID;
    public string description;
    public MissionType missionType;
    public int targetAmount;
    public int rewardExp;
    public int skipCost;
}


public enum MissionType
{
    Jump,
    CollectCoin, 
    Score,
}

[System.Serializable]
public class Mission
{
    public MissionData data;
    public int currentAmount;
    public bool isCompleted => currentAmount >= data.targetAmount;
}