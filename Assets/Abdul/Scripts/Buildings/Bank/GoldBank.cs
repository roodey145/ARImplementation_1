using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBank : SliderBarsController
{
    private static List<GoldStorage> _storages = new List<GoldStorage>();
    public static GoldBank instance;

    private new void Awake()
    {
        base.Awake();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal static void RegisterStorage(GoldStorage goldStorage)
    {
        _storages.Add(goldStorage);
        instance._UpdateData();
    }

    internal static void RemoveStorageData(GoldStorage goldStorage)
    {
        for (int i = 0; i < _storages.Count; i++)
        {
            if (_storages[i] == goldStorage)
            {
                _storages.RemoveAt(i);
            }
        }

        instance._UpdateData();
    }

    internal static void UpdateData()
    {
        instance._UpdateData();
    }

    private void _UpdateData()
    {
        int capacity = 0;
        for(int i = 0;  i < _storages.Count; i++)
        {
            capacity += _storages[i].GetCapacity();
        }
        _capacity = capacity;
        UpdateSlider();
    }

    
}
