using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Buyable
{
    [Header("Buyable Settings")]
    public int price;
    public ResourcesType resourcesType;

    [Header("Production Time Settings")]
    public int productionTimeInSeconds;

    private ResourceBank _bank;

    private void _ReferenceBank()
    {
        switch(resourcesType)
        {
            case ResourcesType.Gold:
                _bank = GoldBank.GetInstance();
                break;
        }
    }


    /// <summary>
    /// Checks if the bank with the specified resource type has enough resources to cover for the price of the item.
    /// </summary>
    /// <returns>True if there is enough resources to buy the item, false otherwise.</returns>
    public bool IsThereEnoughResourceToBuy()
    {
        if(_bank == null) _ReferenceBank();
        return _bank.HasResources(price);
    }

    /// <summary>
    /// Takes the required money from the bank.
    /// </summary>
    /// <returns>Return whether there were enough money to buy the item or not.</returns>
    public virtual bool Buy()
    {
        if(!IsThereEnoughResourceToBuy()) return false;

        _bank.IncreaseDecreaseValue(-price);

        return true;
    }
}
