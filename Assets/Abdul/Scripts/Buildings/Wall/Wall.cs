using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Wall : UpgradeableBuildingData
{
    // Get access to the NavMeshSurface to update it
    private NavMeshSurface _navMeshSurface;

    [Header("Defence")]
    [SerializeField] private Defence _defence;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        _navMeshSurface = GameObject.FindGameObjectWithTag("Ground").GetComponent<NavMeshSurface>();
        _defence = GetComponent<Defence>();
        _defence.UpdateStats(_upgradeData.health, 0);
        StartCoroutine(_BakeNavMeshSurface());
    }

    protected override void _GetUpgradeData()
    {
        _upgradeData = WallLevelsData.GetInstance().GetLevelData(_level);
    }

    internal override void Upgrade()
    {
        base.Upgrade();

        // Upgrade the data
        _defence.UpdateStats(_upgradeData.health, 0);
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


    private IEnumerator _BakeNavMeshSurface()
    {
        yield return new WaitForSeconds(1);
        _navMeshSurface.BuildNavMesh();
    }
}
