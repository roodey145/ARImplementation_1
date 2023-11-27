using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoldMineController : MonoBehaviour
{
    [SerializeField] private string _goldBankTagName = "GoldBank";
    [SerializeField] private ProgressAnimatorController _goldProgressController;
    [SerializeField] private int _capacity = 500; // Gold capacity
    [SerializeField] private float _goldPerHour = 1250;
    [SerializeField] private float _updateRateInSeconds = 10; // Will update the amount of gold each 10 sec
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private InputActionReference _clickAction;

    private ColorChangeHoverInteractor _interactor;

    private int SECONDS_PER_HOUR = 60 * 60;

    internal float goldMined { get; private set; } = 0;

    private SliderBarsController _goldBank;

    private int _level = 1;
    private BuildingData _buildingData;

    private void Awake()
    {
        // Get access to the buildingData
        _buildingData = GetComponent<BuildingData>();
        _buildingData.RegisterLevelUpdateCallback(_LevelUp);
    }

    // Start is called before the first frame update
    void Start()
    {
        _interactor = GetComponent<ColorChangeHoverInteractor>();
        _goldBank = GameObject.FindGameObjectWithTag(_goldBankTagName).GetComponent<SliderBarsController>();

        // Update the progress bar data
        _UpdateData();

        // Register the method to be executed when the click action has be fired
        if (_clickAction != null) _clickAction.action.performed += _CollectResrouces;

        StartCoroutine(_UpdateGoldAmount());

        if(_text ==  null)
        {
            _text = GetComponentsInChildren<TextMeshProUGUI>()[0];
        }

        if(_goldProgressController == null)
        {
            _goldProgressController = GetComponentInChildren<ProgressAnimatorController>();
        }

        _UpdateUI();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private IEnumerator _UpdateGoldAmount()
    {
        yield return new WaitForSeconds(_updateRateInSeconds);

        goldMined += _updateRateInSeconds / SECONDS_PER_HOUR * _goldPerHour;

        if (goldMined > _capacity)
            goldMined = _capacity;

        _UpdateUI();

        _goldProgressController.UpdateAnimationProgress(goldMined / _capacity);

        StartCoroutine(_UpdateGoldAmount());
    }


    private void _UpdateUI()
    {
        _text.text = $"{(int)goldMined}/{_capacity}";
    }

    private void _CollectResrouces(InputAction.CallbackContext context)
    {
        if(_interactor.hovered == true && !context.canceled)
        { // Collect the resources
            // The IncreaseDecreaseValue method returns the remining gold if the GoldStorages are filled
            goldMined = _goldBank.IncreaseDecreaseValue(goldMined); // Reset
            _UpdateUI();
        }
    }


    private void _LevelUp(int level)
    {
        _level = level;
        _UpdateData();
    }

    private void _UpdateData()
    {
        GoldMineLevelData storageData = GoldMineLevelsData.GetStorageData(_level);

        _capacity = storageData.capacity;
        _goldPerHour = storageData.productionSpeed;
        _buildingData.AssignUpgradeCost(storageData.cost);
        _buildingData.AssignUpgradeTime(storageData.upgradeTimeInSeconds);

        _UpdateUI();
    }

    private void RetriveDataFromDatabase()
    {
        
    }
}
