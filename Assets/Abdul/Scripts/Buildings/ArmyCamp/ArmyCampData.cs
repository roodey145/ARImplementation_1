using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCampData : UpgradeableBuildingData
{
    [Header("Attack")]
    [SerializeField] private Defence _defence;

    // This will allow barracks to add warriors to this camp
    private static List<ArmyCampData> _camps = new List<ArmyCampData>();

    //internal static Vector3 AddWarrior(WarriorData warriorData)
    //{

    //}

    protected new void Awake()
    {
        base.Awake();
        _camps.Add(this);
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
