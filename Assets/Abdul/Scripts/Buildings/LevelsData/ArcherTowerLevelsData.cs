using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerLevelsData : BuildingLevelsData
{
    protected ArcherTowerLevelsData() { }

    private static ArcherTowerLevelsData _instance;

    public static ArcherTowerLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ArcherTowerLevelsData();
        }

        return _instance;
    }

    public new ArcherTowerLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (ArcherTowerLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new ArcherTowerLevelData[]
            {
                new ArcherTowerLevelData()
                {
                    level = 1,
                    cost = 15,
                    health = 25,
                    attackDamage = 2,
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
                new ArcherTowerLevelData()
                {
                    level = 2,
                    cost = 25,
                    health = 50,
                    attackDamage = 5,
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
                new ArcherTowerLevelData()
                {
                    level = 3,
                    cost = 50,
                    health = 125,
                    attackDamage = 15,
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
                },
                new ArcherTowerLevelData()
                {
                    level = 3,
                    cost = 100,
                    health = 250,
                    attackDamage = 25,
                    upgradeTimeInSeconds = 240,
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
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Damage,
                                maxValue = 500,
                                currentValue = 25,
                                extraValueAfterUpgrade = 25,
                            }
                        }
                    }
                },
                new ArcherTowerLevelData()
                {
                    level = 4,
                    cost = 250,
                    health = 500,
                    attackDamage = 50,
                    upgradeTimeInSeconds = 600,
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
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Damage,
                                maxValue = 500,
                                currentValue = 50,
                                extraValueAfterUpgrade = 75,
                            }
                        }
                    }
                },
                new ArcherTowerLevelData()
                {
                    level = 5,
                    cost = 500,
                    health = 1000,
                    attackDamage = 125,
                    upgradeTimeInSeconds = 15 * 60,
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
                                extraValueAfterUpgrade = 0,
                            },
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Damage,
                                maxValue = 500,
                                currentValue = 125,
                                extraValueAfterUpgrade = 0,
                            }
                        }
                    }
                }
            };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class ArcherTowerLevelData : BuildingLevelData
{
    public int attackDamage;
}
