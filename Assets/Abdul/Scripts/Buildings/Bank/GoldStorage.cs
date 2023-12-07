using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStorage : UpgradeableBuildingData
{
    // The capcity should be calculated using pre-determined meausres
    [SerializeField] private int _capacity;

    [Header("Defence")]
    [SerializeField] private Defence _defence;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        _UpdateData();

        _defence = GetComponent<Defence>();
        _defence.UpdateStats(_upgradeData.health, 0);

        // Register the storage
        GoldBank.GetInstance().RegisterStorage(this);
    }


    // Getter
    internal int GetCapacity()
    {
        return _capacity;
    }

    internal override void LevelUp()
    {
        base.LevelUp();
        _UpdateData();
    }

    private void _UpdateData()
    {
        GoldStorageLevelData storageData = GoldStorageLevelsData.GetInstance().GetLevelData(_level);
        _capacity = storageData.capacity;

        GoldBank.GetInstance().UpdateData();
    }

    private void OnDestroy()
    {
        GoldBank.GetInstance().RemoveStorageData(this);
    }

    internal override void Upgrade()
    {
        base.Upgrade();

        // Upgrade the data
        _defence.UpdateStats(((ArcherTowerLevelData)_upgradeData).health, 0);
    }
}
