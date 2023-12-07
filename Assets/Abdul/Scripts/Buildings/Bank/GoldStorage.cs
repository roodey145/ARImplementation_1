using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStorage : UpgradeableBuildingData
{
    // The capcity should be calculated using pre-determined meausres
    [SerializeField] private int _capacity;


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        _UpdateData();

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
}
