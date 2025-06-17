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


   private void Start()
   {

      RankManager.Instance.OnDataChanged += Refresh;
   }

   public void Refresh()
   {

      Debug.Log("Refresh");
      
      Top20s = RankManager.Instance.Ranking;
      
      Debug.Log("Top20s Count: " + Top20s.Count);
      for (int i = 0; i < 20; i++)
      {
         Top20s_Bar[i].Refresh(Top20s[i].Nickname, Top20s[i].Score, i+1);
      }
      
      playerBarUI.Refresh(PlayerDTO.Nickname, PlayerDTO.Score, PlayerDTO.RankNumber);
   }
}
