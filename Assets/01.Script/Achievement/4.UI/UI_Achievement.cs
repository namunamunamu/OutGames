using System.Collections.Generic;
using UnityEngine;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField] private List<UI_AchievementSlot> _slots;

    [SerializeField] private UI_AchievementSlot SlotPrefab;
    [SerializeField] private Transform SlotHolderTransform;

    private List<AchievementDTO> _achievements;

    private void Start()
    {
        AchievementManager.Instance.OnDataChange += Refresh;
        Init();
    }

    private void Init()
    {
        _achievements = AchievementManager.Instance.Achievements;
        if (_slots.Count == 0)
        {
            foreach (AchievementDTO achievementDTO in _achievements)
            {
                UI_AchievementSlot slot = Instantiate(SlotPrefab, SlotHolderTransform);
                slot.Refresh(achievementDTO);
                _slots.Add(slot);
            }
        }
    }

    private void Refresh()
    {
        _achievements = AchievementManager.Instance.Achievements;
        for (int i = 0; i < _achievements.Count; ++i)
        {
            _slots[i].Refresh(_achievements[i]);
        }
    }
}
