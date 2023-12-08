using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GoldBank : SliderBarsController
{
    private static List<GoldStorage> _storages = new List<GoldStorage>();
    private static GoldBank _instance;

    private List<Action<int>> _goldUpdatedCallbacks = new List<Action<int>>();

    internal void RegisterGoldUpdatedCallback(Action<int> callback)
    {
        // Add the callback so it can be notified when the gold amount changes
        _goldUpdatedCallbacks.Add(callback);
    }

    internal void UnregisterGoldUpdatedCallback(Action<int> callback)
    {
        // Remove the callback so it does not cause any errors
        _goldUpdatedCallbacks.Remove(callback);
    }


    internal override void SetValue(float value)
    {
        base.SetValue(value);
        _NotifyResourcesUpdatedCallBacks();
    }

    internal override float IncreaseDecreaseValue(float value)
    {
        value = base.IncreaseDecreaseValue(value);

        _NotifyResourcesUpdatedCallBacks();

        return value;
    }

    private void _NotifyResourcesUpdatedCallBacks()
    {
        for (int i = 0; i < _goldUpdatedCallbacks.Count; i++)
        {
            if (_goldUpdatedCallbacks[i] != null)
                _goldUpdatedCallbacks[i]((int)_value);
        }
    }

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
