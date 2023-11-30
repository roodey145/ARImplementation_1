using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStorage : MonoBehaviour
{
    // The capcity should be calculated using pre-determined meausres
    [SerializeField] private int _capacity;
    private int _level = 1;

    private BuildingData _buildingData;
    // Start is called before the first frame update
    private void Start()
    {
        _UpdateData();

        // Register the storage
        GoldBank.GetInstance().RegisterStorage(this);

        // Get access to the buildingData
        _buildingData = GetComponent<BuildingData>();
        _buildingData.RegisterLevelUpdateCallback(_LevelUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Getter
    internal int GetCapacity()
    {
        return _capacity;
    }

    private void _LevelUp(int level)
    {
        _level = level;
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
