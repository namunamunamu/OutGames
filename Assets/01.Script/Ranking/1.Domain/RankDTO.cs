using System;
using UnityEngine;

public class RankDTO 
{
   public readonly int Score;
   public readonly int RankNumber;
   public readonly string Nickname;
   
   
   public RankDTO(int score, int rankNumber, string nickname)
   {
      if (score < 0)
      {
         throw new Exception("점수는 음수가 될 수 없습니다.");
      }

      if (rankNumber < 0)
      {
         throw new Exception("점수는 음수가 될 수 없습니다.");
      }

      if (string.IsNullOrEmpty(nickname))
      {
         throw new Exception("닉넴이 비었어용");
      }
      
      this.Score = score;
      this.RankNumber = rankNumber;
      this.Nickname = nickname;
   }
}
