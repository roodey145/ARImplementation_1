using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBank : SliderBarsController
{
    private static List<GoldStorage> _storages = new List<GoldStorage>();
    private static GoldBank _instance;

    protected new void Awake()
    {
        base.Awake();
        _instance = this;
    }

    public static GoldBank GetInstance()
    {
        return _instance;
    }

    #region Register And Remove Storages
    internal void RegisterStorage(GoldStorage goldStorage)
    {
        // Add the storage to the list
        _storages.Add(goldStorage);
        // Update the capacity and the visual of the slider
        _instance.UpdateData();
    }

    internal void RemoveStorageData(GoldStorage goldStorage)
    {
        // Remove the GoldStorage from the list
        for (int i = 0; i < _storages.Count; i++)
        {
            if (_storages[i] == goldStorage)
            {
                _storages.RemoveAt(i);
                break;
            }
        }
        // Update the capacity and the visual of the slider
        _instance.UpdateData();
    }
    #endregion

    /// <summary>
    /// Set the new capacity and updates the visual to match the new capacity
    /// </summary>
    internal void UpdateData()
    {
        // Set the new capacity and updates the visual to match the new capacity
        SetCapacity(_CalculateCapacity());
    }

    /// <summary>
    /// Calculates the capacity by calculating the sum of the registered storages capacity.
    /// </summary>
    /// <returns>The capacity</returns>
    private int _CalculateCapacity()
    {
        int capacity = 0;
        for (int i = 0; i < _storages.Count; i++)
        {
            capacity += _storages[i].GetCapacity();
        }
        return capacity;
    }
}
