using System;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
   public static RankManager Instance;

   public List<RankDTO> Ranking;
   public RankDTO PlayerDto => _rank.ToDTO();
   
   private Rank _rank;
   public event Action OnDataChanged;
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this);
      }


      Init();
   }

   private void Init()
   {
      //Repo에서 PlayerDTO를 Load 받아옴
      if (TryGetPlayerDto())
      {
         
      }
      else
      {

         AccountDTO accountDto = AccountManager.Instance.CurrentAccount;
         _rank = new Rank(0, 0, accountDto.Nickname); //RankNumber를 처음에 0으로 하는 것에 대해서 논의가 필요함
      }
   }

   public bool TryGetPlayerDto()
   {
      
      
      
      
      
      
      OnDataChanged?.Invoke();
      return true;
   }
   
}
