using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMineLevelsData : BuildingLevelsData
{
    protected GoldMineLevelsData() { }

    private static GoldMineLevelsData _instance;
    public static GoldMineLevelsData GetInstance()
    {
        if(_instance == null)
        {
            _instance = new GoldMineLevelsData();
        }

        return _instance;
    }
    public new GoldMineLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (GoldMineLevelData)_buildingLevelsData[level - 1];
    }

    protected override void _RetrieveData()
    {
        _buildingLevelsData = new GoldMineLevelData[]
        {
            new GoldMineLevelData()
            {
                level = 1,
                capacity = 500,
                productionSpeed = 2500,
                cost = 10,
                upgradeTimeInSeconds = 10,
                resourcesType = ResourcesType.Gold,
            },
            new GoldMineLevelData()
            {
                level = 2,
                capacity = 1250,
                productionSpeed = 3750,
                cost = 25,
                upgradeTimeInSeconds = 20,
                resourcesType = ResourcesType.Gold,
            },
            new GoldMineLevelData()
            {
                level = 3,
                capacity = 2500,
                productionSpeed = 5000,
                cost = 50,
                upgradeTimeInSeconds = 30,
                resourcesType = ResourcesType.Gold,
            },
            new GoldMineLevelData()
            {
                level = 4,
                capacity = 5000,
                productionSpeed = 6250,
                cost = 100,
                upgradeTimeInSeconds = 60,
                resourcesType = ResourcesType.Gold,
            },
            new GoldMineLevelData()
            {
                level = 5,
                capacity = 10000,
                productionSpeed = 12500,
                cost = 250,
                upgradeTimeInSeconds = 60 * 5,
                resourcesType = ResourcesType.Gold,
            }
        };
        _maxLevel = _buildingLevelsData.Length;
    }
}


public class GoldMineLevelData : BuildingLevelData
{
    internal int level;
    internal int capacity;
    internal int productionSpeed;
}