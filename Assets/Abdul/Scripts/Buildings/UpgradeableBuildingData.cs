using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeableBuildingData : InteractableBuilding
{
    [SerializeField] private int _updateRateInSeconds = 1;
    [SerializeField] protected BuildingLevelData _upgradeData;
    [SerializeField] private BuildingLevelsData _data;
    [SerializeField] protected ResourceBank _bank;


    protected new void Awake()
    {
        base.Awake();
        // Get the required data to upgrade this building
        _GetUpgradeData();
        print(_upgradeData.resourcesType);
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }


    protected virtual void _GetUpgradeData()
    {
        _upgradeData = GetLevelsData().GetLevelData(_level);
    }

    internal virtual void Upgrade()
    {
        // Level up
        LevelUp();
        // Get the data to the new upgrade
        _GetUpgradeData();
    }

    #region Getters
    /// <summary>
    /// Get the cost to the next upgrade.
    /// </summary>
    /// <returns>The resources required to upgrade the building</returns>
    internal int GetCost()
    {
        return _upgradeData.cost;
    }

    /// <summary>
    /// Gets the required time for the upgration of the building.
    /// </summary>
    /// <returns>Required time in seconds to complete upgrade.</returns>
    internal int GetUpgradeTimeInSeconds()
    {
        return _upgradeData.upgradeTimeInSeconds;
    }

    /// <summary>
    /// The rate in which the visual are updated.
    /// </summary>
    /// <returns>The time in seconds.</returns>
    internal int GetUpdateRateInSeconds()
    {
        return _updateRateInSeconds;
    }

    /// <summary>
    /// Gets the resources type required to the cost
    /// </summary>
    /// <returns>The resources type</returns>
    internal ResourcesType GetResourcesType()
    {
        return _upgradeData.resourcesType;
    }

    /// <summary>
    /// Gets the resource bank, which can be used to withdraw the required amount to upgrade the building ,of the specified resourceType.
    /// </summary>
    /// <returns>The resource bank of the required upgrade resourceType.</returns>
    internal ResourceBank GetBank()
    {
        if(_bank == null)
        {
            switch(GetResourcesType())
            {
                case ResourcesType.Gold:
                    _bank = GoldBank.GetInstance();
                    break;
                case ResourcesType.Elixir:
                    throw new NotImplementedException("The Elixir Resource Bank has not been implemented yet!");
                    break;
            }
        }
        return _bank;
    }

    /// <summary>
    /// Gets the building level data of this particular building type.
    /// </summary>
    /// <returns>The building levels data.</returns>
    internal BuildingLevelsData GetLevelsData()
    {
        BuildingLevelsData levelsData = _data;

        // Get the levels data of this particular building type
        if(_data == null)
        {
            switch (_buildingType)
            {
                case BuildingType.GoldMine:
                    levelsData = GoldMineLevelsData.GetInstance();
                    break;
                case BuildingType.MoneyBank:
                    levelsData = GoldStorageLevelsData.GetInstance();
                    break;
            }

        }
        _data = levelsData;

        return levelsData;
    }

    internal bool IsMaxLevel()
    {
        return _data.IsMaxLevel(_level);
    }
    #endregion
}
