using UnityEngine;

[System.Serializable]
public class StageLevelDTO
{
    public ELevels StageLevelType;      
    public float ThresholdTime;

    public float SpawnTime;
    public float HPAmount;
    public int SpawnAmount;

    
    
    public StageLevelDTO(StageLevel stageLevel)
    {
        StageLevelType = stageLevel.StageLevelType;
        ThresholdTime = stageLevel.StageThreshholdTime;
        

        SpawnTime = stageLevel.SpawnTime;
        HPAmount = stageLevel.HPAmount;
        SpawnAmount = stageLevel.SpawnAmount;
    }
}