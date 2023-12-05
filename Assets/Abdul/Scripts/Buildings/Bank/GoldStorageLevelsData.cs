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
                health = 100,
                cost = 15,
                capacity = 2500,
                upgradeTimeInSeconds = 30,
                resourcesType = ResourcesType.Gold,
                upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                {
                    featuresDetails = new UpgradeFeatureDetails[]
                    {
                        new UpgradeFeatureDetails()
                        {
                            feature = UpgradeFeature.Health,
                            maxValue = 10000,
                            currentValue = 100,
                            extraValueAfterUpgrade = 150,
                        },
                        new UpgradeFeatureDetails()
                        {
                            feature = UpgradeFeature.GoldStorage,
                            maxValue = 1_000_000,
                            currentValue = 2500,
                            extraValueAfterUpgrade = 2500,
                        }
                    }
                }
            },
            new GoldStorageLevelData()
            {
                level = 2,
                health = 250,
                cost = 50,
                capacity = 5000,
                upgradeTimeInSeconds = 60,
                resourcesType = ResourcesType.Gold,
                upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                {
                    featuresDetails = new UpgradeFeatureDetails[]
                    {
                        new UpgradeFeatureDetails()
                        {
                            feature = UpgradeFeature.Health,
                            maxValue = 10_000,
                            currentValue = 250,
                            extraValueAfterUpgrade = 320,
                        },
                        new UpgradeFeatureDetails()
                        {
                            feature = UpgradeFeature.GoldStorage,
                            maxValue = 1_000_000,
                            currentValue = 5000,
                            extraValueAfterUpgrade = 5000,
                        }
                    }
                }
            },
            new GoldStorageLevelData()
            {
                level = 3,
                health = 570,
                cost = 250,
                capacity = 10000,
                upgradeTimeInSeconds = 150,
                resourcesType = ResourcesType.Gold,
                upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                {
                    featuresDetails = new UpgradeFeatureDetails[]
                    {
                        new UpgradeFeatureDetails()
                        {
                            feature = UpgradeFeature.Health,
                            maxValue = 10_000,
                            currentValue = 570,
                            extraValueAfterUpgrade = 430,
                        },
                        new UpgradeFeatureDetails()
                        {
                            feature = UpgradeFeature.GoldStorage,
                            maxValue = 1_000_000,
                            currentValue = 10_000,
                            extraValueAfterUpgrade = 15_000,
                        }
                    }
                }
            }
        };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class GoldStorageLevelData : BuildingLevelData
{
    internal int capacity;
}