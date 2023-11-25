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

    private MeshRenderer _renderer;
    // Start is called before the first frame update
    protected void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        SetValue(_value);


        // Get the text if its not already assigned
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }


    internal void SetValue(float value)
    {
        _value = value;
        UpdateSlider();        
    }

    internal void IncreaseDecreaseValue(float value)
    {
        _value += value;
        UpdateSlider();
    }

    internal float GetValueInPercentage()
    {
        return _value / _capacity;
    }

    internal void UpdateSlider()
    {
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
