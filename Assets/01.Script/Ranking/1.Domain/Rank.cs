using UnityEngine;

public class Rank
{
   public int Score;
   public int RankNumber;
   public string Nickname;

   public RankDTO ToDTO(int score, int rankNumber, string nickname)
   {
      
      return new RankDTO(score, rankNumber, nickname);
   }
}
