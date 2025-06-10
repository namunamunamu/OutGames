using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gpm.Ui;

public class UI_AchievementSlot : InfiniteScrollItem
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardAmountTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI ProgressSliderText;
    public TextMeshProUGUI RewardClaimDate;
    public Button RewardClaimButton;

    private AchievementDTO _achievementDTO;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);
    }

    public void Refresh(AchievementDTO achievement)
    {
        _achievementDTO = achievement;
        NameTextUI.text = achievement.Name;
        DescriptionTextUI.text = achievement.Description;
        RewardAmountTextUI.text = achievement.RewardAmount.ToString();
        ProgressSlider.maxValue = achievement.GoalValue;
        ProgressSlider.value = achievement.CurrentValue;
        ProgressSliderText.text = $"{achievement.CurrentValue} / {achievement.GoalValue}";

        RewardClaimButton.interactable = achievement.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // 성공 vfx
        }
        else
        {
            // 진행도가 부족합니다 팝업
        }
        
    }
}
