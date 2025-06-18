
public class AttendanceRewardDTO
{
    public readonly ECurrencyType RewardCurrencyType;
    public readonly int RewardCurrencyAmount;
    public bool IsClaimed;
    public bool CanClaim;

    public AttendanceRewardDTO(ECurrencyType currencyType, int rewardCurrencyAmount, bool isClaimed, bool canClaim)
    {
        RewardCurrencyType = currencyType;
        RewardCurrencyAmount = rewardCurrencyAmount;
        IsClaimed = isClaimed;
        CanClaim = canClaim;
    }

    public AttendanceRewardDTO(AttendanceReward attendanceReward, bool canClaim)
    {
        RewardCurrencyType = attendanceReward.CurrencyType;
        RewardCurrencyAmount = attendanceReward.Amount;
        IsClaimed = attendanceReward.IsClaimed;
        CanClaim = canClaim;
    }
}
