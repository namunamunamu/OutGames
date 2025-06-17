using System.Collections.Generic;
using System;
using UnityEngine;


public class RankRepository
{
    public List<RankDTO> RankList { get; private set; }
    public RankDTO PlayerRank { get; private set; }

    private const string SAVE_SERVER_KEY = nameof(RankRepository) + "_SERVER";

    public RankRepository()
    {
        RankList = new List<RankDTO>();
        RankList = LoadServerData();
    }

    public void Save(RankDTO playerRank)
    {
        if (playerRank == null)
        {
            throw new Exception("플레이어 랭크 DTO가 없습니다!");
        }

        PlayerRank = playerRank;

        int playerIndex = RankList.FindIndex(x => x.Nickname == PlayerRank.Nickname);
        if (playerIndex == -1)
        {
            RankList.Add(PlayerRank);
        }
        else
        {
            RankList[playerIndex] = PlayerRank;
        }
        RankList.Sort();
        SaveServerData();
    }

    public RankDTO Load(string name)
    {
        PlayerRank = FindRank(name);
        return PlayerRank;
    }

    public RankDTO FindRank(string name)
    {
        return RankList.Find(x => x.Nickname == name);
    }

    private void SaveServerData()
    {
        ServerSaveData saveData = new ServerSaveData(RankList);
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_SERVER_KEY, json);
    }

    private List<RankDTO> LoadServerData()
    {
        List<RankDTO> loadedData = new List<RankDTO>();
        if (PlayerPrefs.HasKey(SAVE_SERVER_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_SERVER_KEY);
            loadedData = JsonUtility.FromJson<List<RankDTO>>(json);
        }
        else
        {
            for (int i = 0; i < 30; i++)
            {
                RankDTO rank = new RankDTO(i * 100, 0, $"Tester{i}");
                RankList.Add(rank);
            }
        }
        return loadedData;
    }
}

public class ServerSaveData
{
    public List<RankDTO> RankList;

    public ServerSaveData(List<RankDTO> rankList)
    {
        RankList = rankList;
    }
}

