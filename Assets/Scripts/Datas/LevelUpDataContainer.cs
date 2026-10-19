using UnityEngine;
using System.Collections.Generic;

public class LevelUpDataContainer : MonoBehaviour
{
    [SerializeField] List<LevelUpData> datas = new List<LevelUpData>();

    Dictionary<LevelUpType, LevelUpData> dicDatas = new Dictionary<LevelUpType, LevelUpData>();

    private void Awake()
    {
        foreach(LevelUpData data in datas)
        {
            dicDatas.Add(data.levelUpType, data);
        }
    }

    public LevelUpData GetLevelUpDataByType(LevelUpType type)
    {
        if(dicDatas.ContainsKey(type) == true)
        {
            return dicDatas[type];
        }

        return null;
    }

    public List<LevelUpData> Clone()
    {
        return new List<LevelUpData>(datas);
    }
}
