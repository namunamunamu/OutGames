using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceRewardSO", menuName = "Scriptable Objects/AttendanceRewardSO")]
public class AttendanceRewardSO : ScriptableObject
{
    public int AttendanceDate;
    public ECurrencyType CurrencyType;
    public int CurrencyAmount;
}
