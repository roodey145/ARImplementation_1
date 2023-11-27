using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SliderBarsController : MonoBehaviour
{
    [SerializeField] protected int _capacity = 500;
    [SerializeField] protected float _value = 150;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private MeshRenderer _renderer;
    // Start is called before the first frame update
    protected void Awake()
    {
        print("Awake");
        _renderer = GetComponent<MeshRenderer>();
        SetValue(_value);


        // Get the text if its not already assigned
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }


    internal void SetCapacity(int capacity)
    {
        _capacity = capacity;
    }

    internal void SetValue(float value)
    {
        _value = value;
        UpdateSlider();
    }

    /// <summary>
    /// Store the gold in the storage and returns any gold that excceds the maximum storage cpacity.
    /// </summary>
    /// <param name="value">The mined amount of gold.</param>
    /// <returns>The remining gold if the storage is filled.</returns>
    internal float IncreaseDecreaseValue(float value)
    {
        float remining = 0;
        // Check if the value exceds the capacity
        if((value + _value) > _capacity)
        {
            remining = (value + _value) - _capacity; // Make sure no gold will be wasted
            _value = _capacity;
        }
        else
        {
            _value += value;
        }

        UpdateSlider();

        return remining;
    }

    internal float GetValueInPercentage()
    {
        return _value / _capacity;
    }

    internal bool HasResources(float value)
    {
        return _value >= value;
    }


    internal void UpdateSlider()
    {
        if (_renderer == null)
            print("Renderer is not assigned");
        _renderer.material.SetFloat("_Value", GetValueInPercentage());

        _UpdateUI();
    }

    private void _UpdateUI()
    {
        _text.text = $"{(int)_value} / {_capacity}";
    }

    protected void UpdateCapacity(int capacity)
    {
        _capacity = capacity;
        UpdateSlider();
    }
}
