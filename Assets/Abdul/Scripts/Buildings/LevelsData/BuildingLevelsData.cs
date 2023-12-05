using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public abstract class BuildingLevelsData
{
    protected BuildingLevelData[] _buildingLevelsData;
    protected int _maxLevel = 3;

    public virtual BuildingLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return _buildingLevelsData[level - 1];
    }

    public virtual bool IsMaxLevel(int level)
    {
        return (level >= _maxLevel);
    }

    protected abstract void _RetrieveData();
}

[Serializable]
public class BuildingLevelData
{
    internal int level;
    internal int cost;
    internal int health;
    internal int upgradeTimeInSeconds;
    internal ResourcesType resourcesType;
    internal UpgradeFeaturesDetails upgradeFeaturesDetails;
}
