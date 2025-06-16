using System;
using UnityEngine;

public class StageRepo
{

    public void Save()
    {

       StageDTO dto = StageManager.instance.GetCurrentStageDTO();
     StageSaveData stageSaveData = new StageSaveData(dto);
     string jsonString = JsonUtility.ToJson(stageSaveData);
     PlayerPrefs.SetString("StageSave", jsonString);
    }

    public StageDTO Load()
    {
        string json = PlayerPrefs.GetString("StageSave");
        StageDTO dto = JsonUtility.FromJson<StageDTO>(json);
        return dto;
    }
}

[Serializable]
public struct StageSaveData
{
    public StageDTO DTO;

    public StageSaveData(StageDTO dto)
    {
        DTO = dto;
    }
}