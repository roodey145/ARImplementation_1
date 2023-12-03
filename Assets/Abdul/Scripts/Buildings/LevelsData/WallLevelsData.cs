using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLevelsData : BuildingLevelsData
{
    protected WallLevelsData() { }

    private static WallLevelsData _instance;

    public static WallLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new WallLevelsData();
        }

        return _instance;
    }

    public new WallLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (WallLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new WallLevelData[]
            {
                new WallLevelData()
                {
                    level = 1,
                    cost = 15,
                    health = 10,
                    upgradeTimeInSeconds = 5,
                    resourcesType = ResourcesType.Gold,
                },
                new WallLevelData()
                {
                    level = 2,
                    cost = 50,
                    health = 25,
                    upgradeTimeInSeconds = 10,
                    resourcesType = ResourcesType.Gold,
                },
                new WallLevelData()
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

public class WallLevelData : BuildingLevelData
{

}
