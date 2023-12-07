using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : UpgradeableBuildingData
{
    [Header("Defence")]
    [SerializeField] private Defence _defence;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        _defence = GetComponent<Defence>();
        _defence.UpdateStats(_upgradeData.health, 0);
    }

    protected override void _GetUpgradeData()
    {
        _upgradeData = TownHallLevelsData.GetInstance().GetLevelData(_level);
    }

    internal override void AssignIndestructible(Indestructible indestructible)
    {

        base.AssignIndestructible(indestructible);

        AssignLLevel(_indestructibleInfo.level);
        // Get the data to the new upgrade
        _GetUpgradeData();

        appliedX = _indestructibleInfo.appliedX;
        appliedZ = _indestructibleInfo.appliedZ;
    }

    internal override void Upgrade()
    {
        base.Upgrade();

        // Upgrade the data
        _defence.UpdateStats(((ArcherTowerLevelData)_upgradeData).health, 0);
    }
}
