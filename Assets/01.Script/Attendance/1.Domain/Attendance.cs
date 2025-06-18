using System;
using System.Collections.Generic;

public class Attendance
{
    public readonly string ID;
    public readonly DateTime StartDate;                         // 출석 이벤트와 같이 작은 이벤트의 경우 미리 패치하여 데이터만 가지고 있고, 시작 전까지는 UI를 숨김
    public DateTime LastAttendanceDate { get; private set; }    // 마지막 출석일
    public int AttendanceCount { get; private set; }            // 출석 일수

    private List<AttendanceReward> _rewards;
    public List<AttendanceReward> Rewards;

    public List<ECurrencyType> RewardCurrencyTypes;
    public List<int> RewardCurrencyAmount;
    public List<bool> RewardClaimed;

    public Attendance(string id, DateTime startDate, DateTime lastAttendanceDate, int attendanceCount)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("ID는 비어있을 수 없습니다.");
        }
        if (startDate == new DateTime()) // 구조체의 경우 값 변수이기 때문에 null 검사를 수행할 수 없음. 대신, 구조체의 초기값 검사를 통해 유효성 검사 가능
            {
                throw new Exception("출석 시작일이 필요합니다.");
            }

        if (attendanceCount < 0)
        {
            throw new Exception("출석일수는 0보다 작을 수 없습니다.");
        }

        if (lastAttendanceDate == new DateTime())
        {
            throw new Exception($"마지막 출석일자는 null일 수 없습니다.");
        }

        if (lastAttendanceDate < startDate)
        {
            throw new Exception($"마지막 출석일자는 {startDate}보다 이전일 수 없습니다.");
        }

        ID = id;
        StartDate = startDate;
        AttendanceCount = attendanceCount;
        LastAttendanceDate = lastAttendanceDate;

        _rewards = new List<AttendanceReward>();
    }

    public void CheckAttendance(DateTime date)
    {
        if (date == new DateTime())
        {
            throw new Exception("출석 DateTime이 지정되지 않았습니다.");
        }

        // TODO: year과 month도 비교한다.
        if (LastAttendanceDate.Month < date.Month || LastAttendanceDate.Year < date.Year || LastAttendanceDate.Day < date.Day)
        {
            ++AttendanceCount;
            LastAttendanceDate = date;
        }
    }

    public void AddReward(AttendanceReward reward)
    {
        if (reward == null)
        {
            throw new Exception("출석 보상이 null일 수 없습니다.");
        }

        _rewards.Add(reward);
    }

    public bool TryClaim(int day)
    {
        if (day < 0 || _rewards.Count <= day)
        {
            throw new Exception("출석 인덱스가 유효하지 않습니다.");
        }

        if (AttendanceCount < day)
        {
            return false;
        }

        return _rewards[day - 1].TryClaim();
    }

    public AttendanceDTO ToDTO()
    {
        return new AttendanceDTO(this);
    }
}
