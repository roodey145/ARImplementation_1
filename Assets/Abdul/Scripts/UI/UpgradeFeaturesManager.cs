using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeFeature
{
    Health,
    GoldStorage,
    Speed,
}

public class UpgradeFeaturesManager : MonoBehaviour
{
    [SerializeField] private SliderController[] _features;

    // Start is called before the first frame update
    void Start()
    {
        _features = GetComponentsInChildren<SliderController>();
    }

    public void AssignFeatures(UpgradeFeature[] features)
    {

    }
}
