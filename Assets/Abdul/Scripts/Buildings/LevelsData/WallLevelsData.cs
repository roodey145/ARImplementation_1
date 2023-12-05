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
                    health = 25,
                    upgradeTimeInSeconds = 5,
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
                            }
                        }
                    }
                },
                new WallLevelData()
                {
                    level = 2,
                    cost = 25,
                    health = 50,
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
                                currentValue = 50,
                                extraValueAfterUpgrade = 75,
                            }
                        }
                    }
                },
                new WallLevelData()
                {
                    level = 3,
                    cost = 50,
                    health = 125,
                    upgradeTimeInSeconds = 15,
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
                            }
                        }
                    }
                },
                new WallLevelData()
                {
                    level = 4,
                    cost = 100,
                    health = 250,
                    upgradeTimeInSeconds = 15,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 5_000,
                                currentValue = 250,
                                extraValueAfterUpgrade = 250,
                            }
                        }
                    }
                },
                new WallLevelData()
                {
                    level = 5,
                    cost = 250,
                    health = 500,
                    upgradeTimeInSeconds = 15,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 5_000,
                                currentValue = 500,
                                extraValueAfterUpgrade = 500,
                            }
                        }
                    }
                },
                new WallLevelData()
                {
                    level = 6,
                    cost = 500,
                    health = 1000,
                    upgradeTimeInSeconds = 15,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 5_000,
                                currentValue = 1000,
                                extraValueAfterUpgrade = 750,
                            }
                        }
                    }
                }
            };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class WallLevelData : BuildingLevelData
{

}
