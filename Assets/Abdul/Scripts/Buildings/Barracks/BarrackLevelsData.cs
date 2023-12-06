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
                    cost = 150,
                    health = 75,
                    upgradeTimeInSeconds = 60,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 2_400,
                                currentValue = 75,
                                extraValueAfterUpgrade = 125,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Speed,
                                maxValue = 90,
                                currentValue = 0,
                                extraValueAfterUpgrade = 5,
                            }
                        }
                    }
                },
                new BarrackLevelData()
                {
                    level = 2,
                    cost = 350,
                    health = 200,
                    upgradeTimeInSeconds = 30,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 2_400,
                                currentValue = 200,
                                extraValueAfterUpgrade = 300,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Speed,
                                maxValue = 90,
                                currentValue = 0,
                                extraValueAfterUpgrade = 5,
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
                                maxValue = 2_400,
                                currentValue = 75,
                                extraValueAfterUpgrade = 125,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Speed,
                                maxValue = 90,
                                currentValue = 0,
                                extraValueAfterUpgrade = 5,
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
    internal int speedIncrease;
}
