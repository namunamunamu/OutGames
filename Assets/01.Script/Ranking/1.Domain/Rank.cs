using System;
using UnityEngine;

public class Rank
{
   private int _score;
   public int Score => _score;
   
   private int _rankNumber;
   public int RankNumber=> _rankNumber;
   
   private string _nickname;
   public string Nickname => _nickname;



   public void AddScore(int amount)
   {
      if (amount < 0)
      {
         throw new Exception("더하는 점수는 양수여야합니다.");
      }
      
      _score += amount;
   }
   
   
   
   public Rank(int score, int rankNumber, string nickname)
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
      
      _score = score;
      _rankNumber = rankNumber;
      _nickname = nickname;
   }
   
   
   public RankDTO ToDTO()
   {
      
      return new RankDTO(_score, _rankNumber, _nickname);
   }
}
