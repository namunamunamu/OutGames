using System;
using UnityEngine;

public class StageRepo
{

    public StageSaveData Save()
    {
     StageSaveData stageSaveData = new StageSaveData();
     
    }

    public StageDTO Load(StageSaveData saveData)
    {
        
    }
}

[Serializable]
public struct StageSaveData
{
    public int StageNumber;
    public 
}