using System;

[Serializable]
public class AchievementDTO
{
    public string ID;
    public string Name;
    public string Description;
    public EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    public int CurrentValue;
    public bool RewardClaimed;

    public AchievementDTO(string id, int currentValue, bool rewardClaimed)
    {
        ID = id;
        CurrentValue = currentValue;
        RewardClaimed = rewardClaimed;
    }

    public AchievementDTO(Achievement achievement)
    {
        ID = achievement.Id;
        Name = achievement.Name;
        Description = achievement.Description;
        Condition = achievement.Condition;
        GoalValue = achievement.GoalValue;
        RewardCurrencyType = achievement.RewardCurrencyType;
        RewardAmount = achievement.RewardAmount;
        CurrentValue = achievement.Currentvalue;
        RewardClaimed = achievement.RewardClaimed;
    }

    public bool CanClaimReward()
    {
        return RewardClaimed == false && CurrentValue >= GoalValue;
    }
}
