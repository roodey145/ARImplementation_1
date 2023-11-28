using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;


public abstract class BuildingLevelsData
{
    protected static BuildingLevelData[] _buildingLevelsData;
    protected static int _maxLevel = 3;

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
    internal int cost;
    internal int upgradeTimeInSeconds;
    internal ResourcesType resourcesType;
}
