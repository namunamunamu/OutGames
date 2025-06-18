using System;
using System.Collections.Generic;

public class AttendanceDTO
{
    public string ID { get; }
    public DateTime StartDate { get; }
    public DateTime LastAttendanceDate { get; }
    public int AttendanceCount { get;}

    public List<AttendanceRewardDTO> Rewards { get; }


    // 직렬화를 위한 기본 생성자
    public AttendanceDTO(string id, DateTime startDate, DateTime lastAttendanceDate, int attendanceCount, List<AttendanceReward> rewards)
    {
        ID = id;
        StartDate = startDate;
        LastAttendanceDate = lastAttendanceDate;
        AttendanceCount = attendanceCount;
        Rewards = new List<AttendanceRewardDTO>();

        // foreach (var reward in rewards)
        // {
        //     Rewards.Add(new AttendanceRewardDTO(reward));
        // }

        for (int i = 0; i < rewards.Count; i++)
        {
            bool canClaim = !rewards[i].IsClaimed && i >= attendanceCount;
            Rewards.Add(new AttendanceRewardDTO(rewards[i].CurrencyType, rewards[i].Amount, rewards[i].IsClaimed, canClaim));
        }
    }

    // Attendance 객체를 AttendanceDto로 쉽게 변환하기 위한 생성자
    public AttendanceDTO(Attendance attendance)
    {
        if (attendance == null)
        {
            throw new ArgumentNullException(nameof(attendance), "Attendance DTO 생성 시 Attendance 객체는 null일 수 없습니다.");
        }

        ID = attendance.ID;
        StartDate = attendance.StartDate;
        LastAttendanceDate = attendance.LastAttendanceDate;
        AttendanceCount = attendance.AttendanceCount;
        Rewards = new List<AttendanceRewardDTO>();
        
        for (int i = 0; i < attendance.Rewards.Count; i++)
        {
            bool canClaim = !attendance.Rewards[i].IsClaimed && i >= attendance.AttendanceCount;
            Rewards.Add(new AttendanceRewardDTO(attendance.Rewards[i].CurrencyType, attendance.Rewards[i].Amount, attendance.Rewards[i].IsClaimed, canClaim));
        }
    }


    public Attendance ToAttendance()
    {
        Attendance attendance = new Attendance(ID, StartDate, LastAttendanceDate, AttendanceCount);

        // attendance.Rewards = Rewards;

        return attendance;
    }

}
