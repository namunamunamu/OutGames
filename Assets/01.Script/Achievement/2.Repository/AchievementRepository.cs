using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementRepository
{
    private const string SAVE_KEY = nameof(AchievementRepository);

    public void SaveAchievement(List<AchievementDTO> achievementDTOs)
    {
        AchievementSaveDataList dataList = new AchievementSaveDataList();
        dataList.DataList = achievementDTOs.ConvertAll(data => new AchievementSaveData
        {
            ID = data.ID,
            CurrentValue = data.CurrentValue,
            RewardClaimed = data.RewardClaimed
        });

        string json = JsonUtility.ToJson(dataList);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementSaveData> LoadAcheivement()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        AchievementSaveDataList dataList = JsonUtility.FromJson<AchievementSaveDataList>(json);
        return dataList.DataList;
    }
}

[Serializable] public struct AchievementSaveData
{
    public string ID;
    public int CurrentValue;
    public bool RewardClaimed;
}


[Serializable] public struct AchievementSaveDataList
{
    public List<AchievementSaveData> DataList;   
}
