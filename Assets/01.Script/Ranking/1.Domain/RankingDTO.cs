using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class RankingDTO
{
    public readonly List<RankDTO> RankingList;
    public readonly RankDTO PlayerRank;

    public RankingDTO(List<RankDTO> ranking)
    {
        RankingList = ranking;
    }
}
