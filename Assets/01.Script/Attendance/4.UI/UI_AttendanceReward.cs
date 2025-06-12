using TMPro;
using UnityEngine;

public class UI_AttendanceReward : MonoBehaviour
{
    public TextMeshProUGUI RewardNameText;
    public TextMeshProUGUI RewardAmountText;
    public TextMeshProUGUI AttedanceDateText;
    public GameObject ClaimedIcon;

    private AttendanceRewardDTO _attendanceRewardDTO;

    public void Refresh(AttendanceRewardDTO attendaceRewardDTO)
    {
        _attendanceRewardDTO = attendaceRewardDTO;
        RewardNameText.text = attendaceRewardDTO.RewardCurrency.Type.ToString();
        RewardAmountText.text = attendaceRewardDTO.RewardCurrency.Value.ToString();
        AttedanceDateText.text = $"D{attendaceRewardDTO.AttendanceDate}";

        if (attendaceRewardDTO.IsClaimed)
        {
            ClaimedIcon.SetActive(true);
        }
        
    }

    public void OnClickSlot()
    {
        if (!AttendanceManager.Instance.TryGetReward(_attendanceRewardDTO))
        {
            // 실패 메시지 토스
            return;
        }

        // 보상 획득 vfx 실행
    }
}
