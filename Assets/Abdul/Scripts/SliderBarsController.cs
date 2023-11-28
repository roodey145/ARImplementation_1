using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SliderBarsController : ResourceBank
{
    [Header("Slider Shader And Text References")]
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private MeshRenderer _renderer;

    #region MonoBehavior Related Methods
    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();

        // Get the mesh renderer to be able to adjust the value of the slider.
        _renderer = GetComponent<MeshRenderer>();

        // Update the slider data
        UpdateSlider();

        // Get the text if its not already assigned
        if(_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
    #endregion

    #region Setters overriden from ResourceBank
    internal override void SetCapacity(int capacity)
    {
        base.SetCapacity(capacity);

        // Update the slider info to match with the new capacity
        UpdateSlider();
    }

    internal override void SetValue(float value)
    {
        base.SetValue(value);
        
        // Update the slider to match the new stored resources
        UpdateSlider();
    }

    /// <summary>
    /// Store the gold in the storage and returns any gold that excceds the maximum storage cpacity.
    /// </summary>
    /// <param name="value">The mined amount of gold.</param>
    /// <returns>The remining gold if the storage is filled.</returns>
    internal override float IncreaseDecreaseValue(float value)
    {
        float remining = base.IncreaseDecreaseValue(value);

        // Update the slider data to match the new stored resources
        UpdateSlider();

        // Return the remining
        return remining;
    }
    #endregion

    #region Visual Update Related Methods
    internal void UpdateSlider()
    {
        _renderer.material.SetFloat("_Value", GetValueInPercentage());

        _UpdateUI();
    }

    private void _UpdateUI()
    {
        _text.text = $"{(int)_value} / {_capacity}";
    }
    #endregion
}
