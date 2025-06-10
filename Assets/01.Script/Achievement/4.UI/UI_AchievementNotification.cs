using TMPro;
using UnityEngine;
using DG.Tweening;


public class UI_AchievementNotification : MonoBehaviour
{
    public GameObject AchievementNotification;
    public TextMeshProUGUI AchievementNameText;
    public TextMeshProUGUI AchievementDescriptionText;
    public TextMeshProUGUI AchievementRewardAmountText;

    public RectTransform notificationRectTransform;

    [Header("Animation Settings")]
    public float slideDuration = 0.5f;
    public float visibleDuration = 2f;
    public Vector2 hiddenPosition = new Vector2(0, 300); // 시작 위치 (화면 위)
    public Vector2 visiblePosition = new Vector2(0, -100); // 보여질 위치

    private void Start()
    {
        AchievementManager.Instance.OnNewAchievementRewared += Show;

        // 시작 위치로 세팅
        notificationRectTransform.anchoredPosition = hiddenPosition;
        AchievementNotification.SetActive(false);
    }

    public void Show(AchievementDTO achievementDTO)
    {
        AchievementNotification.SetActive(true);

        AchievementNameText.text = achievementDTO.Name;
        AchievementDescriptionText.text = achievementDTO.Description;
        AchievementRewardAmountText.text = achievementDTO.RewardAmount.ToString();

        // TODO
        // Dotween
        notificationRectTransform.anchoredPosition = hiddenPosition;
        notificationRectTransform
        .DOAnchorPos(visiblePosition, slideDuration)
        .SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            DOVirtual.DelayedCall(visibleDuration, () =>
            {
                notificationRectTransform
                .DOAnchorPos(hiddenPosition, slideDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    AchievementNotification.SetActive(false);
                });
            });
        });
    }
}
