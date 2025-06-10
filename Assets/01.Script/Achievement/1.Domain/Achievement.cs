
using System;

public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Tirgger,

}

public class Achievement
{
    // 데이터
    public readonly string Id;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    // 상태
    private int _currentValue;
    public int Currentvalue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;


    // 생성자
    public Achievement(AchievementSO metaData, AchievementSaveData saveData)
    {
        if (string.IsNullOrEmpty(metaData.Id))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }

        if (metaData.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야합니다.");
        }

        if (metaData.RewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야합니다.");
        }

        if (saveData.CurrentValue < 0)
        {
            throw new Exception("현재 업적 값은 0보다 커야합니다.");
        }

        Id = metaData.Id;
        Name = metaData.Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;

        _currentValue = saveData.CurrentValue;
        _rewardClaimed = saveData.RewardClaimed;
    }

    public void Increase(int value)
    {
        if (value < 0)
        {
            throw new Exception("증가 값은 0보다 커야합니다");
        }
        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return _rewardClaimed == false && _currentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if (!CanClaimReward())
        {
            return false;
        }

        _rewardClaimed = true;
        return true;
    }
}
