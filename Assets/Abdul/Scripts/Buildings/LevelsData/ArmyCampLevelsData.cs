using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCampLevelsData : BuildingLevelsData
{
    protected ArmyCampLevelsData() { }

    private static ArmyCampLevelsData _instance;

    public static ArmyCampLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ArmyCampLevelsData();
        }

        return _instance;
    }

    public new ArmyCampLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (ArmyCampLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new ArmyCampLevelData[]
            {
                new ArmyCampLevelData()
                {
                    level = 1,
                    cost = 500,
                    health = 75,
                    upgradeTimeInSeconds = 60 * 5,
                    resourcesType = ResourcesType.Gold,
                    soliderCapacity = 20,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 1200,
                                currentValue = 75,
                                extraValueAfterUpgrade = 50,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.SoliderCapacity,
                                maxValue = 100,
                                currentValue = 20,
                                extraValueAfterUpgrade = 5,
                            }
                        }
                    }
                },
                new ArmyCampLevelData()
                {
                    level = 2,
                    cost = 1000,
                    health = 125,
                    upgradeTimeInSeconds = 60 * 15,
                    resourcesType = ResourcesType.Gold,
                    soliderCapacity = 25,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 1200,
                                currentValue = 125,
                                extraValueAfterUpgrade = 75,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.SoliderCapacity,
                                maxValue = 100,
                                currentValue = 25,
                                extraValueAfterUpgrade = 5,
                            }
                        }
                    }
                },
                new ArmyCampLevelData()
                {
                    level = 3,
                    cost = 15_000,
                    health = 200,
                    upgradeTimeInSeconds = 60 * 30,
                    resourcesType = ResourcesType.Gold,
                    soliderCapacity = 30,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 1200,
                                currentValue = 200,
                                extraValueAfterUpgrade = 100,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.SoliderCapacity,
                                maxValue = 100,
                                currentValue = 30,
                                extraValueAfterUpgrade = 10,
                            }
                        }
                    }
                }
            };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class ArmyCampLevelData : BuildingLevelData
{
    internal int soliderCapacity;
}