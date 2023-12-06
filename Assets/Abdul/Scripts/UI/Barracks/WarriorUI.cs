using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorUI : MonoBehaviour
{
    [SerializeField] internal WarriorData warriorData = new WarriorData();
    [SerializeField] private Button _buyButton;
    [SerializeField] private WarriorsProductionListManager _warriorProductionManager;

    // Start is called before the first frame update
    void Start()
    {
        _SetupButton();
    }

    internal void Unrestrict()
    {
        _buyButton.enabled = true;
    }

    internal void AssignProductionManager(WarriorsProductionListManager warriorProductionManager)
    {
        _warriorProductionManager = warriorProductionManager;
    }

    private void _SetupButton()
    {
        if (_buyButton == null) _buyButton = GetComponentInChildren<Button>();

        if (_buyButton == null) print("Buy Button is still null!!");

        // Register a method for when the button is clicked.
        _buyButton.onClick.AddListener(_Buy);

        _buyButton.enabled = false;
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

        }
        else
        { // Play a sound to indicate that the warrior could not be bought

        }
    }
}
