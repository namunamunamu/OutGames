using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;




public class RankUI : MonoBehaviour
{
   public List<RankDTO> Top20s = new List<RankDTO>();
   public List<RankBar_UI> Top20s_Bar = new List<RankBar_UI>();
   
   
   public RankDTO PlayerDTO;
   public RankBar_UI playerBarUI;
   
   
   public void Refresh()
   {
      for (int i = 0; i < Top20s.Count; i++)
      {
         Top20s_Bar[i].Refresh(Top20s[i].Nickname, Top20s[i].Score, Top20s[i].RankNumber);
      }
      
      playerBarUI.Refresh(PlayerDTO.Nickname, PlayerDTO.Score, PlayerDTO.RankNumber);
   }
}
