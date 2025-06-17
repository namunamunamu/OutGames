using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class StageLevelDTO
{
    public ELevels StageLevelType;
    public float StartTime;
    public float EndTime;

    public float SpawnTime;
    public float HPAmount;
    public int SpawnAmount;

    
    
    public StageLevelDTO(StageLevel stageLevel)
    {
        StageLevelType = stageLevel.StageLevelType;
        StartTime = stageLevel.StageStartTime;
        EndTime = stageLevel.StageEndTime;
        

        SpawnTime = stageLevel.SpawnTime;
        HPAmount = stageLevel.HPAmount;
        SpawnAmount = stageLevel.SpawnAmount;
    }
}