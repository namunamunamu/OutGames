using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceRewardSO", menuName = "Scriptable Objects/AttendanceRewardSO")]
public class AttendanceRewardSO : ScriptableObject
{
    [SerializeField] private int _attendanceIndex;
    public int AttendanceIndex => _attendanceIndex;

    [SerializeField] private ECurrencyType _currencyType;
    public ECurrencyType CurrencyType => _currencyType;

    [SerializeField] private int _currencyAmount;
    public int CurrencyAmount => _currencyAmount;
}
