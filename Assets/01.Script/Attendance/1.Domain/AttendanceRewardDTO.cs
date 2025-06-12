
public class AttendanceRewardDTO
{
    public readonly int AttendanceDate;
    public readonly CurrencyDTO RewardCurrency;
    public bool IsClaimed;

    public AttendanceRewardDTO(AttendanceReward attendanceReward)
    {
        AttendanceDate = attendanceReward.AttendanceDate;
        RewardCurrency = attendanceReward.RewardCurrency;
        IsClaimed = attendanceReward.IsClaimed;
    }
}
