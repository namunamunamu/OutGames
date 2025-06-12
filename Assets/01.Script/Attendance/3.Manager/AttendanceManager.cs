using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceManager : MonoBehaviour
{
    public static AttendanceManager Instance;

    private AttendanceRepository _repository;

    private List<AttendanceReward> _attendanceRewardList;
    public List<AttendanceRewardDTO> AttendanceRewardList => _attendanceRewardList.ConvertAll(a => new AttendanceRewardDTO(a));

    [SerializeField] private List<AttendanceRewardSO> _attendanceRewardSOList;

    private int _currentAttendanceDate;
    private int _rewardClaimedAttendanceDate;



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


        AttendanceSaveData loadedData = _repository.Load(AccountManager.Instance.CurrentAccount.Email);
        if (loadedData == null)
        {
            _currentAttendanceDate = 1;
            _rewardClaimedAttendanceDate = 0;
        }
        else
        {
            _currentAttendanceDate = ++loadedData.LastAttendanceDate;
            _rewardClaimedAttendanceDate = loadedData.RewardClaimedAttendanceDate;
        }

        _repository.Save(_currentAttendanceDate, _rewardClaimedAttendanceDate, AccountManager.Instance.CurrentAccount.Email);
        InitAttendanceRewards();
    }

    public void InitAttendanceRewards()
    {
        _attendanceRewardList = new List<AttendanceReward>();

        for (int i = 0; i < _attendanceRewardSOList.Count; ++i)
        {
            CurrencyDTO reward = new CurrencyDTO(_attendanceRewardSOList[i].CurrencyType, _attendanceRewardSOList[i].CurrencyAmount);
            AttendanceReward attendanceReward = new AttendanceReward(i + 1, reward, _attendanceRewardSOList[i].AttendanceDate <= _rewardClaimedAttendanceDate); // isClaimed 정보를 reop에서 로드해야함
            _attendanceRewardList.Add(attendanceReward);
        }
    }

    public bool TryGetReward(AttendanceRewardDTO desireAttendance)
    {
        // 보상가능여부 평가
        if (desireAttendance.AttendanceDate > _currentAttendanceDate)
        {
            Debug.LogError($"선택한 출석 일자 {desireAttendance.AttendanceDate}는 현재 출석 일수 {_currentAttendanceDate} 보다 큽니다.");
            return false;
        }

        // 지금까지 안받은 보상 받기
        for (int i = _rewardClaimedAttendanceDate; i < desireAttendance.AttendanceDate; ++i)
        {
            CurrencyDTO reward = _attendanceRewardList[i].RewardCurrency;
            CurrencyManager.Instance.AddCurrency(reward.Type, reward.Value);
            _attendanceRewardList[i].GetReward();

            Debug.Log($"{_attendanceRewardList[i].AttendanceDate}일차 보상 획득 완료!");
        }

        // 보상 받은 출석일자 업데이트
        _rewardClaimedAttendanceDate = desireAttendance.AttendanceDate;

        _repository.Save(_currentAttendanceDate, _rewardClaimedAttendanceDate, AccountManager.Instance.CurrentAccount.Email);

        OnDataChanged?.Invoke();
        return true;
    }
    
}
