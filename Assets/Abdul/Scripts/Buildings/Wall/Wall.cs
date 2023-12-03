using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : UpgradeableBuildingData
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    protected override void _GetUpgradeData()
    {
        _upgradeData = WallLevelsData.GetInstance().GetLevelData(_level);
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
}
