using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Attendance : MonoBehaviour
{
    public UI_AttendanceReward SlotPrefab;
    public Transform SlotParentTransfrom;
    private List<UI_AttendanceReward> _slots;

    public void Start()
    {
        AttendanceManager.Instance.OnDataChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        // List<AttendanceRewardDTO> attendanceRewardDTOList = AttendanceManager.Instance.AttendanceRewardList;
        // if (_slots == null)
        // {
        //     _slots = new List<UI_AttendanceReward>();
        //     for (int i = 0; i < attendanceRewardDTOList.Count; ++i)
        //     {
        //         UI_AttendanceReward slot = Instantiate(SlotPrefab, SlotParentTransfrom);
        //         slot.Refresh(attendanceRewardDTOList[i]);
        //         _slots.Add(slot);
        //     }
        // }

        // for (int i = 0; i < attendanceRewardDTOList.Count; ++i)
        // {
        //     _slots[i].Refresh(attendanceRewardDTOList[i]);
        // }

        // AttendanceDTO attendance = AttendanceManager.Instance.GetAttendance("7days");
        // foreach (var slot in _slots)
        // {
        //     slot.Refresh(attendance.Rewards[0]);
        // }
    }

    public void OnClickNexButton()
    {
        SceneManager.LoadScene(2);
    }
}
