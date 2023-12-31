using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallLevelsData : BuildingLevelsData
{
    protected TownHallLevelsData() { }

    private static TownHallLevelsData _instance;

    public static TownHallLevelsData GetInstance()
    {
        if (_instance == null)
        {
            _instance = new TownHallLevelsData();
        }

        return _instance;
    }

    public new TownHallLevelData GetLevelData(int level)
    {
        if (level > _maxLevel)
        {
            level = _maxLevel;
        }

        if (_buildingLevelsData == null)
        {
            _RetrieveData();
        }

        return (TownHallLevelData)_buildingLevelsData[level - 1];
    }


    protected override void _RetrieveData()
    {
        _buildingLevelsData = new TownHallLevelData[]
            {
                new TownHallLevelData()
                {
                    level = 1,
                    cost = 500,
                    health = 1200,
                    upgradeTimeInSeconds = 5,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 25_000,
                                currentValue = 1200,
                                extraValueAfterUpgrade = 500,
                            }
                        }
                    }
                },
                new TownHallLevelData()
                {
                    level = 2,
                    cost = 2500,
                    health = 1700,
                    upgradeTimeInSeconds = 10,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 25_000,
                                currentValue = 1700,
                                extraValueAfterUpgrade = 1700,
                            }
                        }
                    }
                },
                new TownHallLevelData()
                {
                    level = 3,
                    cost = 12_500,
                    health = 3400,
                    upgradeTimeInSeconds = 15,
                    resourcesType = ResourcesType.Gold,
                    upgradeFeaturesDetails = new UpgradeFeaturesDetails()
                    {
                        featuresDetails = new UpgradeFeatureDetails[]
                        {
                            new UpgradeFeatureDetails()
                            {
                                feature = UpgradeFeature.Health,
                                maxValue = 25_000,
                                currentValue = 3400,
                                extraValueAfterUpgrade = 1600,
                            }
                        }
                    }
                }
            };
        _maxLevel = _buildingLevelsData.Length;
    }
}

public class TownHallLevelData : BuildingLevelData
{

}
