using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeFeaturesDetails
{
    public UpgradeFeatureDetails[] featuresDetails;
}

[Serializable]
public class UpgradeFeatureDetails
{
    public UpgradeFeature feature;
    public float maxValue;
    public float currentValue;
    public float extraValueAfterUpgrade;
}
