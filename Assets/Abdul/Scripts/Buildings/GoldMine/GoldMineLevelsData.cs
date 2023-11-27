using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMineLevelsData 
{
    private static GoldMineLevelData[] _data;
    private static int _maxLevel = 3;

    public static GoldMineLevelData GetStorageData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_data == null)
        {
            _RetrieveData();
        }

        return _data[level - 1];
    }

    private static void _RetrieveData()
    {
        _data = new GoldMineLevelData[]
        {
            new GoldMineLevelData()
            {
                level = 1,
                capacity = 500,
                productionSpeed = 2500,
                cost = 10,
                upgradeTimeInSeconds = 10,
            },
            new GoldMineLevelData()
            {
                level = 2,
                capacity = 1250,
                productionSpeed = 3750,
                cost = 25,
                upgradeTimeInSeconds = 20,
            },
            new GoldMineLevelData()
            {
                level = 3,
                capacity = 2500,
                productionSpeed = 5000,
                cost = 50,
                upgradeTimeInSeconds = 30,
            },
            new GoldMineLevelData()
            {
                level = 4,
                capacity = 5000,
                productionSpeed = 6250,
                cost = 100,
                upgradeTimeInSeconds = 60,
            },
            new GoldMineLevelData()
            {
                level = 5,
                capacity = 10000,
                productionSpeed = 12500,
                cost = 250,
                upgradeTimeInSeconds = 60 * 5,
            }
        };
        _maxLevel = 5;
    }
}


public class GoldMineLevelData
{
    internal int level;
    internal int capacity;
    internal int productionSpeed;
    internal int cost;
    internal int upgradeTimeInSeconds;
}