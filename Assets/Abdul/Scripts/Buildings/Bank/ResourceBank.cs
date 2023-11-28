using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBank : MonoBehaviour
{
    [Header("Bank Data")]
    [SerializeField] protected int _capacity = 500;
    [SerializeField] protected float _value = 150;

    #region MonoBehavior Methods
    protected void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
    #endregion

    #region Setters
    internal virtual void SetCapacity(int capacity)
    {
        _capacity = capacity;
    }

    internal virtual void SetValue(float value)
    {
        _value = value;
    }

    /// <summary>
    /// Store the gold in the storage and returns any resource that excceds the maximum storage cpacity.
    /// </summary>
    /// <param name="value">The mined amount of resources to store.</param>
    /// <returns>The remining resource if the storage is filled.</returns>
    internal virtual float IncreaseDecreaseValue(float value)
    {
        float remining = 0;
        // Check if the value exceds the capacity
        if ((value + _value) > _capacity)
        {
            remining = (value + _value) - _capacity; // Make sure no gold will be wasted
            _value = _capacity;
        }
        else
        {
            _value += value;
        }

        return remining;
    }
    #endregion

    #region Getters
    internal float GetValueInPercentage()
    {
        // Make sure that the capacity is not zero.
        return _capacity == 0 ? 0 : (_value / _capacity);
    }
    #endregion

    #region Checkers
    internal bool HasResources(float value)
    {
        return _value >= value;
    }
    #endregion


}
