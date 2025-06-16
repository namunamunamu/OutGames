using System;
using UnityEngine;

[Serializable]
public class StageDTO
{
    public int StageNumber;
    public StageLevelDTO CurrentStageLevel;

    public StageDTO(Stage stage)
    {
        StageNumber = stage.StageNumber;
        CurrentStageLevel = new StageLevelDTO(stage.CurrentStageLevel);
    }

   
}