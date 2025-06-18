using System;

public class AttendanceReward

{
    // DTO는 데이터 송수신용으로 현재 UI에 데이터를 전달하는데 사용됨 -> UI 기획에 따라 DTO가 들고 있는 데이터의 형태가 변경될 수 있으며, 그 경우 DTO 객체를 들고 있는 모든 클래스가 영향을 받는다.
    // 따라서 DTO를 도메인에서 들고있는 것은 바람직하지 않은 구조
    // private readonly CurrencyDTO _rewardCurrency;
    // public CurrencyDTO RewardCurrency => _rewardCurrency;
    
    private readonly int _attendanceIndex;
    public int AttendanceIndex => _attendanceIndex;

    private ECurrencyType _currencyType;
    public ECurrencyType CurrencyType => _currencyType;

    private int _amount;
    public int Amount => _amount;

    private bool _isClaimed;
    public bool IsClaimed => _isClaimed;


    public AttendanceReward(ECurrencyType currencyType, int amount, bool isClaimed = false)
    {   
        if (amount <= 0)
        {
            throw new Exception("출석 보상이 0 이하일 수 없습니다.");
        }

        _currencyType = currencyType;
        _amount = amount;
        _isClaimed = isClaimed;
    }

    public bool TryClaim()
    {
        if (_isClaimed == true)
        {
            return false;
        }

        _isClaimed = true;
        return true;
    }
}
