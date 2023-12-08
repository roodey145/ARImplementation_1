using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarriorUI : MonoBehaviour
{
    [Header("Sounds Settings")]
    [SerializeField] private string _warriorBoughtSound = "Warrior roar";
    [SerializeField] private string _noEnoughMoneySound = "denied";

    [SerializeField] internal WarriorData warriorData = new WarriorData();
    [SerializeField] private Button _buyButton;
    [SerializeField] private WarriorsProductionListManager _warriorProductionManager;

    // In case the unrestrict method is called before the Start method, this will be used
    // to ensure that the buy button will be enabled either way
    private bool _buyable = false;

    [Header("Price Color Settings")]
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Color _textColorIfHasResources = new Color(255, 255, 68);
    [SerializeField] private Color _textColorIfNoResources = new Color(255, 0, 17);

    // Start is called before the first frame update
    void Start()
    {
        _SetupButton(_buyable);
        // Register a GoldResources update callback
        GoldBank.GetInstance().RegisterGoldUpdatedCallback(_UpdatePriceText);
        _UpdatePriceText(0);
    }

    internal void Unrestrict()
    {
        _buyable = true;
        if (_buyButton != null) _buyButton.interactable = true;
    }

    internal void AssignProductionManager(WarriorsProductionListManager warriorProductionManager)
    {
        _warriorProductionManager = warriorProductionManager;
    }

    private void _SetupButton(bool buyable)
    {
        if (_buyButton == null) _buyButton = GetComponentInChildren<Button>();

        if (_buyButton == null) print("Buy Button is still null!!");

        // Register a method for when the button is clicked.
        _buyButton.onClick.AddListener(_Buy);

        _buyButton.interactable = buyable;
    }


    private void _Buy()
    {
        // Check if there is enough money to buy the warrior
        // TODO: Check if there is enough space in the production wait list to produce a new warrior
        if (warriorData.IsThereEnoughResourceToBuy())
        { // There were enough resources to cover for the warrior expenses
            // Withdraw the warrior costs
            warriorData.Buy();
            // Add the warrior to the produce list
            _warriorProductionManager.AddWarriorToProduce(warriorData);
            // Play the sound of the bough warrior
            SoundManager.Instance.PlayActionSound(_warriorBoughtSound);
        }
        else
        { // Play a sound to indicate that the warrior could not be bought
            SoundManager.Instance.PlayActionSound(_noEnoughMoneySound);
        }
    }


    private void _UpdatePriceText(int currentValuta)
    {
        if(warriorData.IsThereEnoughResourceToBuy())
        {
            _priceText.text = $"<color=#{_textColorIfHasResources.ToHexString()}>{warriorData.price} G</color>";
        }
        else
        {
            _priceText.text = $"<color=#{_textColorIfNoResources.ToHexString()}>{warriorData.price} G</color>";
        }
    }



    private void OnDestroy()
    {
        if (GoldBank.GetInstance() != null)
            GoldBank.GetInstance().UnregisterGoldUpdatedCallback(_UpdatePriceText);
    }
}
