using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackLevelsData : BuildingLevelsData
{
    protected BarrackLevelsData() { }

    private static BarrackLevelsData _instance;

    public static BarrackLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new BarrackLevelsData();
        }

        return _instance;
    }

    public new BarrackLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (BarrackLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new BarrackLevelData[]
            {
                new BarrackLevelData()
                {
                    level = 1,
                    cost = 15,
                    health = 25,
                    upgradeTimeInSeconds = 10,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 5_000,
                                currentValue = 25,
                                extraValueAfterUpgrade = 25,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Damage,
                                maxValue = 500,
                                currentValue = 2,
                                extraValueAfterUpgrade = 3,
                            }
                        }
                    }
                },
                new BarrackLevelData()
                {
                    level = 2,
                    cost = 25,
                    health = 50,
                    upgradeTimeInSeconds = 30,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 5_000,
                                currentValue = 50,
                                extraValueAfterUpgrade = 75,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Damage,
                                maxValue = 500,
                                currentValue = 5,
                                extraValueAfterUpgrade = 10,
                            }
                        }
                    }
                },
                new BarrackLevelData()
                {
                    level = 3,
                    cost = 50,
                    health = 125,
                    upgradeTimeInSeconds = 90,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 5_000,
                                currentValue = 125,
                                extraValueAfterUpgrade = 125,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Damage,
                                maxValue = 500,
                                currentValue = 15,
                                extraValueAfterUpgrade = 10,
                            }
                        }
                    }
                }
            };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class BarrackLevelData : BuildingLevelData
{
    
}
