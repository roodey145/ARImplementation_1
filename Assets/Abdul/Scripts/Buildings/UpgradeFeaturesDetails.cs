using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFeaturesDetails
{
    public List<UpgradeFeatureDetails[]> featuresDetails = new List<UpgradeFeatureDetails[]>();
}

public class UpgradeFeatureDetails
{
    public UpgradeFeature feature;
    public float maxValue;
    public float currentValue;
    public float extraValueAfterUpgrade;
}
