using System;
using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public TextMeshProUGUI StageNumber_Text;
    public TextMeshProUGUI StageTimer_Text;
    public TextMeshProUGUI StageLevel_Text;

    private StageDTO StageDTO;
    
    private void Start()
    {
        StageManager.instance.OnStageLevelChanged += Refresh;
        Refresh();
    }


    private void Update()
    {
        Refresh_Timer();
    }

    public void Refresh()
    {
        StageDTO = StageManager.instance.GetCurrentStageDTO();
        StageNumber_Text.text = StageDTO.StageNumber.ToString();
        StageLevel_Text.text = StageDTO.CurrentStageLevel.StageLevelType.ToString();
//        Debug.Log($"{StageDTO.CurrentStageLevel.StageLevelType.ToString()}, ::: {StageDTO.StageNumber.ToString()}");
        
    }

    public void Refresh_Timer()
    {
        StageTimer_Text.text = StageManager.instance.Timer.ToString();
    }
}
