using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor.Overlays;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField] private List<AchievementSO> MetaDataList;

    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll(a => new AchievementDTO(a));

    private AchievementRepository _repository;

    public event Action OnDataChange;
    public event Action<AchievementDTO> OnNewAchievementRewared;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }

        Init();
    }

    public void Init()
    {
        _repository = new AchievementRepository();
        _achievements = new List<Achievement>();


        List<AchievementSaveData> saveDataLists = _repository.LoadAcheivement();
        foreach (AchievementSO metaData in MetaDataList)
        {
            Achievement duplicatedAchievement = FindByID(metaData.Id);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID: {metaData.Id}가 중복됩니다.");
            }
            AchievementSaveData saveData = saveDataLists != null ? saveDataLists.Find(a => a.ID == metaData.Id) : new AchievementSaveData();
            _achievements.Add(new Achievement(metaData, saveData));
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(a => a.Id == id);
    }

    public void Increase(EAchievementCondition condition, int value)
    {
        foreach (Achievement achievement in _achievements)
        {
            if (achievement.Condition == condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();

                achievement.Increase(value);

                bool canClaimReward = achievement.CanClaimReward();
                if (prevCanClaimReward == false && canClaimReward == true)
                {
                    OnNewAchievementRewared?.Invoke(new AchievementDTO(achievement));
                }
            }
        }

        _repository.SaveAchievement(Achievements);
        
        OnDataChange?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achievementDTO)
    {
        Achievement achievement = FindByID(achievementDTO.ID);
        if (achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        {
            CurrencyManager.Instance.AddCurrency(achievement.RewardCurrencyType, achievement.RewardAmount);
            OnDataChange?.Invoke();
            return true;
        }

        return false;
    }
}
