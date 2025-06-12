using System;
using UnityEngine;

public class AttendanceRepository : MonoBehaviour
{
    private const string SAVE_KEY = nameof(AttendanceRepository);
    public void Save(int currentAttendanceDate, int rewardClaimedAttendanceDate, string userID)
    {
        AttendanceSaveData saveData = new AttendanceSaveData(currentAttendanceDate, rewardClaimedAttendanceDate);
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY + "_" + userID, json);
    }

    public AttendanceSaveData Load(string userID)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + "_" + userID))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + "_" + userID);
        AttendanceSaveData loadedData = JsonUtility.FromJson<AttendanceSaveData>(json);

        return loadedData;
    }
}

[Serializable]
public class AttendanceSaveData
{
    public int LastAttendanceDate;
    public int RewardClaimedAttendanceDate;

    public AttendanceSaveData(int currentAttendanceDate, int rewardClaimedAttendanceDate)
    {
        LastAttendanceDate = currentAttendanceDate;
        RewardClaimedAttendanceDate = rewardClaimedAttendanceDate;
    }
}
