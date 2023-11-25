using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStorageLevelsData
{
    private static GoldStorageLevelData[] _data;
    private static int _maxLevel = 3;

    public static GoldStorageLevelData GetStorageData(int level)
    {
        if(level > _maxLevel)
        {
            level = _maxLevel;
        }

        if(_data == null)
        {
            _RetrieveData();
        }

        return _data[level - 1];
    }

    private static void _RetrieveData()
    {
        _data = new GoldStorageLevelData[]
        {
            new GoldStorageLevelData()
            {
                level = 1,
                capacity = 2500,
            },
            new GoldStorageLevelData()
            {
                level = 2,
                capacity = 5000,
            },
            new GoldStorageLevelData()
            {
                level = 3,
                capacity = 10000,
            }
        };
        _maxLevel = 3;
    }
}

public class GoldStorageLevelData
{
    internal int level;
    internal int capacity;
}