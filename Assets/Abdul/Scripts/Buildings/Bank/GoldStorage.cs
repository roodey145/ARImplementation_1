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
    void Awake()
    {
        // Get access to the building data
        _buildingData = GetComponent<BuildingData>();
        // Register a level up listener
        _buildingData.RegisterLevelUpdateCallback(_LevelUp);
    }

    private void Start()
    {
        _UpdateData();

        // Register the storage
        GoldBank.RegisterStorage(this);
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
        GoldStorageLevelData storageData = GoldStorageLevelsData.GetStorageData(_level);
        _capacity = storageData.capacity;

        GoldBank.UpdateData();
    }


    private void OnDestroy()
    {
        GoldBank.RemoveStorageData(this);
    }
}
