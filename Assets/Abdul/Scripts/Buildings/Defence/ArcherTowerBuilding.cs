using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Defence))]
public class ArcherTowerBuilding : UpgradeableBuildingData
{
    [Header("Attack")]
    [SerializeField] private int _attackDamage = 0;
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
        _attackDamage = ((ArcherTowerLevelData)_upgradeData).attackDamage;
        _defence.UpdateStats(((ArcherTowerLevelData)_upgradeData).health, _attackDamage);
    }
}
