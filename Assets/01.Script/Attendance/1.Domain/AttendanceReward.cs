using System;

public class AttendanceReward
{
    private readonly int _attendanceDate;
    public int AttendanceDate => _attendanceDate;

    private readonly CurrencyDTO _rewardCurrency;
    public CurrencyDTO RewardCurrency => _rewardCurrency;

    private bool _isClaimed;
    public bool IsClaimed => _isClaimed;


    public AttendanceReward(int attendanceDate, CurrencyDTO rewardCurrency, bool isClaimed = false)
    {
        if (attendanceDate <= 0)
        {
            throw new Exception("출석 일자는 0 이하일 수 없습니다.");
        }

        if (rewardCurrency == null)
        {
            throw new Exception("출석 보상 정보가 없습니다!");
        }
        
        if (rewardCurrency.Value <= 0)
        {
            throw new Exception("출석 보상이 0 이하일 수 없습니다.");
        }

        _attendanceDate = attendanceDate;
        _rewardCurrency = rewardCurrency;
        _isClaimed = isClaimed;
    }

    public void GetReward()
    {
        _isClaimed = true;
    }
}
