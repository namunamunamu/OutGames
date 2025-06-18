using TMPro;
using UnityEngine;

public class UI_AttendanceReward : MonoBehaviour
{
    public TextMeshProUGUI RewardNameText;
    public TextMeshProUGUI RewardAmountText;
    public TextMeshProUGUI AttedanceDateText;
    public GameObject ClaimedIcon;

    private AttendanceRewardDTO _attendanceRewardDTO;
    private string _attendanceID;

    public void Refresh(string attendnaceID, AttendanceRewardDTO attendaceRewardDTO)
    {
        _attendanceRewardDTO = attendaceRewardDTO;
        _attendanceID = attendnaceID;

        RewardNameText.text = attendaceRewardDTO.RewardCurrencyType.ToString();
        RewardAmountText.text = $"{attendaceRewardDTO.RewardCurrencyAmount:N0}";
        // AttedanceDateText.text = $"D{attendaceRewardDTO.AttendanceDate}";

        if (attendaceRewardDTO.IsClaimed)
        {
            ClaimedIcon.SetActive(true);
        }

    }

    public void TryRewardClaim()
    {
        AttendanceManager.Instance.TryGetReward(_attendanceID, _attendanceRewardDTO);
    }

    public void OnClickSlot()
    {
        if (!AttendanceManager.Instance.TryGetReward(_attendanceID, _attendanceRewardDTO))
        {
            // 실패 메시지 토스
            return;
        }

        // 보상 획득 vfx 실행
    }
}
