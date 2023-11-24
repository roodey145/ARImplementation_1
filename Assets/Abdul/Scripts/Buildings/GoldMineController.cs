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
    [SerializeField] private InputActionReference _selectAction;

    private ColorChangeHoverInteractor _interactor;

    private int SECONDS_PER_HOUR = 60 * 60;

    internal float goldMined { get; private set; } = 0;

    private SliderBarsController _goldBank;

    // Start is called before the first frame update
    void Start()
    {
        _interactor = GetComponent<ColorChangeHoverInteractor>();
        _goldBank = GameObject.FindGameObjectWithTag(_goldBankTagName).GetComponent<SliderBarsController>();


        // Register the method to be executed when the click action has be fired
        _clickAction.action.performed += _CollectResrouces;

        StartCoroutine(_UpdateGoldAmount());

        if(_text ==  null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
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
            _goldBank.IncreaseDecreaseValue(goldMined);
            goldMined = 0; // Reset
            _UpdateUI();
        }
    }

    private void RetriveDataFromDatabase()
    {
        
    }
}
