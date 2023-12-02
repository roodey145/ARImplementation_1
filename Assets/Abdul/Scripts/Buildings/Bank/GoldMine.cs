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

        StartCoroutine(_MineGold());
        _UpdateUI();
    }



    protected override void Clicked()
    {
        base.Clicked();

        // Collect the resources
        _CollectResrouces();
    }

    private IEnumerator _MineGold()
    {
        // Wait before collecting the new resources
        yield return new WaitForSeconds(_collectingRateInSeconds);

        // Calculate the amount of resources to be added to the collected resources
        collectedResources += _collectingRateInSeconds / _SECONDS_PER_HOUR * _GetResourceCollectingSpeed();

        // Check if there is space to store the collected amount
        if (collectedResources > _GetCapacity())
            collectedResources = _GetCapacity(); // Throw the overflowing resources away

        // Update the resources text
        _UpdateUI();

        // Update the resources animation to indicate how full the storage is.
        _goldProgressController.UpdateAnimationProgress(collectedResources / _GetCapacity());

        StartCoroutine(_MineGold());
    }


    private void _CollectResrouces()
    {
        // The IncreaseDecreaseValue method returns the remining gold if the GoldStorages are filled
        collectedResources = GetBank().IncreaseDecreaseValue(collectedResources); // Reset
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

    
}
