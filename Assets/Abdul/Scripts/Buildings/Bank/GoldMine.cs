using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoldMine : UpgradeableBuildingData
{
    [Header("Gold Mine Data")]
    [SerializeField] private float _collectingRateInSeconds = 10; // Will update the amount of gold each 10 sec
    [SerializeField] internal float collectedResources = 0;

    private readonly int _SECONDS_PER_HOUR = 3600;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private ProgressAnimatorController _goldProgressController;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // TODO: Create a mapper inside the ResourceIndestructible.
        print("ID: " + ID);

        // Assign the resources from the indestructible
        collectedResources = ((ResourceIndestructible)_indestructibleInfo).GetResources();


        // Get the time passed since last update
        _UpdateResources(_CalculateMinedResources((float)_indestructibleInfo.GetTimeDifferenceInSeconds()));

        StartCoroutine(_MineGold());
        _UpdateUI();
    }

    protected override void Clicked()
    {
        base.Clicked();

        // Collect the resources
        _CollectResrouces();
    }

    internal override Indestructible CreateIndestructible(int id)
    {
        if (_indestructibleInfo == null)
        {
            ID = id;
            _indestructibleInfo = new ResourceIndestructible(id, _level, _buildingType, appliedX, appliedZ, collectedResources);
        }

        return _indestructibleInfo;
    }

    private IEnumerator _MineGold()
    {
        // Wait before collecting the new resources
        yield return new WaitForSeconds(_collectingRateInSeconds);

        // Calculate the amount of resources to be added to the collected resources
        _UpdateResources( _CalculateMinedResources(_collectingRateInSeconds) );

        // Start mining again
        StartCoroutine(_MineGold());
    }

    private float _CalculateMinedResources(float timePassed)
    {
        return timePassed / _SECONDS_PER_HOUR * _GetResourceCollectingSpeed();
    }

    private void _UpdateResources(float minedResources)
    {
        // Add the mined resources to the collected resources
        collectedResources += minedResources;

        // Check if there is space to store the collected amount
        if (collectedResources > _GetCapacity())
            collectedResources = _GetCapacity(); // Throw the overflowing resources away

        // Update Indesctructible
        ((ResourceIndestructible)_indestructibleInfo).UpdateResources(collectedResources);

        // Update the resources text
        _UpdateUI();

        // Update the resources animation to indicate how full the storage is.
        _goldProgressController.UpdateAnimationProgress(collectedResources / _GetCapacity());
    }

    private void _CollectResrouces()
    {
        // The IncreaseDecreaseValue method returns the remining gold if the GoldStorages are filled
        collectedResources = GetBank().IncreaseDecreaseValue(collectedResources); // Reset
        // Updates the resources and the last updated time
        ((ResourceIndestructible)_indestructibleInfo).UpdateResources(collectedResources);
        _UpdateUI(); // Update the UI which shows the collected resources amount
    }

    internal override void Upgrade()
    {
        base.Upgrade();
        // Make sure to update the visual to match the new level of the GoldMine
        _UpdateUI();
    }

    private void _UpdateUI()
    {
        _text.text = $"{(int)collectedResources}/{_GetCapacity()}";
    }

    private GoldMineLevelData _GetGoldMineData()
    {
        return ((GoldMineLevelData)_upgradeData);
    }

    private int _GetResourceCollectingSpeed()
    {
        return _GetGoldMineData().productionSpeed;
    }

    private int _GetCapacity()
    {
        return _GetGoldMineData().capacity;
    }

    internal override void AssignIndestructible(Indestructible indestructible)
    {
        try
        {
            _indestructibleInfo = (ResourceIndestructible)indestructible;
        }
        catch(InvalidCastException e)
        {
            //base.AssignIndestructible(indestructible);
            _indestructibleInfo = new ResourceIndestructible(indestructible);
        }
        

        AssignLLevel(_indestructibleInfo.level);
        // Get the data to the new upgrade
        _GetUpgradeData();
        appliedX = _indestructibleInfo.appliedX;
        appliedZ = _indestructibleInfo.appliedZ;
        collectedResources = ((ResourceIndestructible)_indestructibleInfo).GetResources();
    }
}
