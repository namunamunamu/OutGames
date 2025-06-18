using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;

    private AttendanceRepository _repository;

    private List<Attendance> _attendances;

    // private List<AttendanceReward> _attendanceRewardList;
    // public List<AttendanceRewardDTO> AttendanceRewardList => _attendanceRewardList.ConvertAll(a => new AttendanceRewardDTO(a));

    // [SerializeField] private List<AttendanceRewardSO> _attendanceRewardSOList;

    [SerializeField] private List<AttendanceSO> _attendanceSOList;

    private int _currentAttendanceDate;
    private int _rewardClaimedAttendanceDate;
    private DateTime _lastConnectDateTime;



    public event Action OnDataChanged;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void Init()
    {
        _repository = new AttendanceRepository();

        _attendances = new List<Attendance>(_attendanceSOList.Count);

        // DateTime startDay = new DateTime(2025, 6, 1);
        // DateTime lastAttendacneDate = new DateTime(2025, 6, 3);
        // int attendanceCount = 3;

        // Attendance attendance7DayEvent = new Attendance(startDay, lastAttendacneDate, attendanceCount);
        // _attendances.Add(attendance7DayEvent);

        // attendance7DayEvent.AddReward(new AttendanceReward(ECurrencyType.Gold, 100, false));
        // attendance7DayEvent.AddReward(new AttendanceReward(ECurrencyType.Gold, 100, false));
        // attendance7DayEvent.AddReward(new AttendanceReward(ECurrencyType.Gold, 100, false));


        // AttendanceSaveData loadedData = _repository.Load(AccountManager.Instance.CurrentAccount.Email);
        // if (loadedData == null)
        // {
        //     _rewardClaimedAttendanceDate = 0;
        //     _currentAttendanceDate = 1;
        // }
        // else
        // {
        //     _rewardClaimedAttendanceDate = loadedData.RewardClaimedAttendanceDate;

        //     DateTime currentDateTime = DateTime.Now;
        //     if (loadedData.LastConnectDateTime.Date.Day != currentDateTime.Date.Day)
        //     {
        //         _currentAttendanceDate = ++loadedData.LastAttendanceDate;
        //     }
        // }

        // _repository.Save(_currentAttendanceDate, _rewardClaimedAttendanceDate, _lastConnectDateTime, AccountManager.Instance.CurrentAccount.Email);



        DateTime today = DateTime.Today;
        foreach (var attendanceSO in _attendanceSOList)
        {
            if (attendanceSO.StartDate < today)
            {
                continue;
            }

            // if (FindById(attendanceSO.ID != null))
            // {
                
            // }

            Attendance attendance = new Attendance(attendanceSO.ID, attendanceSO.StartDate, today, 1);
            foreach (var attendanceRewardSO in attendanceSO.AttendanceRewards)
            {
                AttendanceReward reward = new AttendanceReward(attendanceRewardSO.CurrencyType, attendanceRewardSO.CurrencyAmount);
                attendance.AddReward(reward);
            }
            _attendances.Add(attendance);
        }

        StartCoroutine(CheckCourutine());
    }

    private Attendance FindById(string id)
    {
        Attendance attendance = _attendances.Find(x => x.ID == id);
        return attendance;
    }

    public AttendanceDTO GetAttendance(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("ID는 비어있을 수 없습니다.");
        }

        Attendance attendance = FindById(id);

        return attendance.ToDTO();
    }

    private IEnumerator CheckCourutine()
    {
        var hourTime = new WaitForSecondsRealtime(60 * 60);

        DateTime today = DateTime.Today;

        foreach (Attendance attendance in _attendances)
        {
            // 묻지 말고 시켜라!
            // 출석일을 비교해서 Count를 올리는 행위
            attendance.CheckAttendance(today);
        }

        OnDataChanged?.Invoke();

        yield return hourTime;
    }

    public bool TryGetReward(string attendanceId, AttendanceRewardDTO desireAttendance)
    {
        // // 보상가능여부 평가
        // if (desireAttendance.AttendanceDate > _currentAttendanceDate)
        // {
        //     Debug.LogError($"선택한 출석 일자 {desireAttendance.AttendanceDate}는 현재 출석 일수 {_currentAttendanceDate} 보다 큽니다.");
        //     return false;
        // }

        // 지금까지 안받은 보상 받기
        // for (int i = _rewardClaimedAttendanceDate; i < desireAttendance.AttendanceDate; ++i)
        // {
        //     CurrencyDTO reward = _attendanceRewardList[i].RewardCurrency;
        //     CurrencyManager.Instance.AddCurrency(reward.Type, reward.Value);
        //     _attendanceRewardList[i].GetReward();

        //     Debug.Log($"{_attendanceRewardList[i].AttendanceDate}일차 보상 획득 완료!");
        // }

        // 보상 받은 출석일자 업데이트
        // _rewardClaimedAttendanceDate = desireAttendance.AttendanceDate;

        _repository.Save(_currentAttendanceDate, _rewardClaimedAttendanceDate, _lastConnectDateTime, AccountManager.Instance.CurrentAccount.Email);

        OnDataChanged?.Invoke();
        return true;
    }
    
}
