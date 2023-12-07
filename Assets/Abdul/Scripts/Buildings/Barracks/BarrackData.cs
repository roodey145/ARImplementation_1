using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackData : UpgradeableBuildingData
{
    [Header("Defence")]
    [SerializeField] private Defence _defence;

    protected new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
        _defence = GetComponent<Defence>();
        _defence.UpdateStats(_upgradeData.health, 0);
    }

    internal override void Upgrade()
    {
        base.Upgrade();

        // Upgrade the data
        _defence.UpdateStats(_upgradeData.health, 0);
    }
}
