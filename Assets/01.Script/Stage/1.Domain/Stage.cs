using System.Collections.Generic;
using UnityEngine;

public class Stage
{
   public int StageNumber;
   private List<StageLevel> _stageLevels = new List<StageLevel>();
   public StageLevel CurrentStageLevel;
   private int _index = 0;

   public Stage(int number, int currentLevelIndex)
   {
         _index = currentLevelIndex;
         StageNumber = number;   
   }

   public void AddStageLevel(StageLevelSO stageLevelSO)
   {
      StageLevel stagelevel = new StageLevel(stageLevelSO);
      _stageLevels.Add(stagelevel);
   }
   
   public bool TryCheckStageLevel(float time)
   {
      
      if (CurrentStageLevel == null)
      {
         CurrentStageLevel = _stageLevels[_index];
      }
      
      float threshholdTime = CurrentStageLevel.StageThreshholdTime;
      if (time >= threshholdTime)
      {
         
         if (_index >= _stageLevels.Count-1) return false;
         _index++;
         
         CurrentStageLevel = _stageLevels[_index];
         
         
         
         return true;
      }
      
      return false;
   }
   
}
