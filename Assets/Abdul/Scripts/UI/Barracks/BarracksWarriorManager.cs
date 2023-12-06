using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksWarriorManager : MonoBehaviour
{
    [SerializeField] private WarriorUI[] _warriors;
    [SerializeField] private UpgradeableBuildingData _buildingData;
    [SerializeField] private WarriorsProductionListManager _warriorProductionManager;

    // Start is called before the first frame update
    void Start()
    {
        _warriors = GetComponentsInChildren<WarriorUI>();

        _buildingData = GetComponentInParent<UpgradeableBuildingData>();

        // Register level up listener
        _buildingData.RegisterLevelUpdateCallback(_UpdateBuyableWarriors);

        // Get the production manager
        _warriorProductionManager = _buildingData.gameObject.GetComponentInChildren<WarriorsProductionListManager>();

        _UpdateBuyableWarriors(_buildingData.GetLevel());
    }

    // Activate the warriors that can be bought
    private void _UpdateBuyableWarriors(int level)
    {
        for(int i = 0; i < _warriors.Length; i++)
        {
            if (_warriors[i].warriorData.requiredBarracksLevel <= level)
            { // The level of the barracks is enough to produce this warrior
                _warriors[i].Unrestrict();
                _warriors[i].AssignProductionManager(_warriorProductionManager);
            }
        }
    }
}
