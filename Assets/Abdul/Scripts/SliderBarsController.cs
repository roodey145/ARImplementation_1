using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SliderBarsController : MonoBehaviour
{
    [SerializeField] private int _maxAmount = 500;
    [SerializeField] private int _value = 150;
    [SerializeField] private TextMeshProUGUI _text;

    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        SetValue(_value);


        // Get the text if its not already assigned
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SetValue(int value)
    {
        _value = value;

        _renderer.material.SetFloat("_Value", GetValueInPercentage());

        _UpdateUI();
    }

    internal float GetValueInPercentage()
    {
        return (float)_value / _maxAmount;
    }

    private void _UpdateUI()
    {
        _text.text = $"{_value} / {_maxAmount}";
    }
}
