using UnityEngine;

public class RankDTO 
{
   public readonly int Score;
   public readonly int RankNumber;
   public readonly string Nickname;

   public RankDTO(int score, int rankNumber, string nickname)
   {
      this.Score = score;
      this.RankNumber = rankNumber;
      this.Nickname = nickname;
   }
}
