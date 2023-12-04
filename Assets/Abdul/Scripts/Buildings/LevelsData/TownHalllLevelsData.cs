using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallLevelsData : BuildingLevelsData
{
    protected TownHallLevelsData() { }

    private static TownHallLevelsData _instance;

    public static TownHallLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new TownHallLevelsData();
        }

        return _instance;
    }

    public new TownHallLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (TownHallLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new TownHallLevelData[]
            {
                new TownHallLevelData()
                {
                    level = 1,
                    cost = 15,
                    health = 10,
                    upgradeTimeInSeconds = 5,
                    resourcesType = ResourcesType.Gold,
                },
                new TownHallLevelData()
                {
                    level = 2,
                    cost = 50,
                    health = 25,
                    upgradeTimeInSeconds = 10,
                    resourcesType = ResourcesType.Gold,
                },
                new TownHallLevelData()
                {
                    level = 3,
                    cost = 250,
                    health = 50,
                    upgradeTimeInSeconds = 15,
                    resourcesType = ResourcesType.Gold,
                }
            };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class TownHallLevelData : BuildingLevelData
{

}
