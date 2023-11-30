using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStorageLevelsData : BuildingLevelsData
{
    protected GoldStorageLevelsData() { }

    private static GoldStorageLevelsData _instance;

    public static GoldStorageLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GoldStorageLevelsData();
        }

        return _instance;
    }

    public new GoldStorageLevelData GetLevelData(int level)
    {
        if(level > _maxLevel)
        {
            level = _maxLevel;
        }

        if(_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (GoldStorageLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new GoldStorageLevelData[]
        {
            new GoldStorageLevelData()
            {
                level = 1,
                capacity = 2500,
                upgradeTimeInSeconds = 30,
                resourcesType = ResourcesType.Gold,
            },
            new GoldStorageLevelData()
            {
                level = 2,
                capacity = 5000,
                upgradeTimeInSeconds = 60,
                resourcesType = ResourcesType.Gold,
            },
            new GoldStorageLevelData()
            {
                level = 3,
                capacity = 10000,
                upgradeTimeInSeconds = 150,
                resourcesType = ResourcesType.Gold,
            }
        };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class GoldStorageLevelData : BuildingLevelData
{
    internal int level;
    internal int capacity;
}