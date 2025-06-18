using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceSO", menuName = "Scriptable Objects/AttendanceSO")]
public class AttendanceSO : ScriptableObject
{
    [SerializeField] private string _id;
    public string ID => _id;

    [SerializeField] private DateTime _startDate;
    public DateTime StartDate => _startDate;

    [SerializeField] private List<AttendanceRewardSO> _attendanceRewards;
    public List<AttendanceRewardSO> AttendanceRewards => _attendanceRewards;
}
