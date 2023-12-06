using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCampData : UpgradeableBuildingData
{
    [Header("Attack")]
    [SerializeField] private Defence _defence;

    protected new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
        _defence = GetComponent<Defence>();
    }

    internal override void Upgrade()
    {
        base.Upgrade();

        // Upgrade the data
        //_defence.UpdateStats(((ArcherTowerLevelData)_upgradeData).health, _attackDamage);
    }
}
