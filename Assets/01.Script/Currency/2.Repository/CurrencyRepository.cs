using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyRepository
{
    // Repository: 데이터의 영속성을 보장
    // 영속성: 프로그램을 종료해도 데이터가 보존되는 것
    // Save/Load

    private const string SAVE_KEY = nameof(CurrencyRepository);

    public void SaveCurrencies(List<CurrencyDTO> dataList, string userID)
    {
        // CSV || JSON || PlayerPrefs

        CurrecnySaveDataList datas = new CurrecnySaveDataList();
        datas.DataList = dataList.ConvertAll(data => new CurrecnySaveData
        {
            Type = data.Type,
            Value = data.Value
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY + "_" + userID, json);
    }

    public List<CurrencyDTO> LoadCurrencies(string userID)
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY + "_" + userID))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY + "_" + userID);
        CurrecnySaveDataList datalist = JsonUtility.FromJson<CurrecnySaveDataList>(json);

        return datalist.DataList.ConvertAll(data => new CurrencyDTO(data.Type, data.Value));
    }
}

[Serializable]
public struct CurrecnySaveData
{
    public ECurrencyType Type;
    public int Value;
}

[Serializable]
public class CurrecnySaveDataList
{
    public List<CurrecnySaveData> DataList;
}
