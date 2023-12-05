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
    [SerializeField] private UpgradeableBuildingData _buildingData;
    [SerializeField] UpgradeFeaturesDetails upgradeFeaturesDetails;
    // Start is called before the first frame update
    void Start()
    {
        _features = GetComponentsInChildren<SliderController>();

        // Hide the features
        for(int i = 0; i < _features.Length; i++)
        {
            _features[i].gameObject.SetActive(false);
        }


        _buildingData = GetComponentInParent<UpgradeableBuildingData>();

        // Register upgrade listeener
        _buildingData.RegisterLevelUpdateCallback(_UpdateData);
        _UpdateData(_buildingData.GetLevel());
    }


    private void _UpdateData(int level)
    {
        // ....
    }


    private void OnEnable()
    {
        if (_buildingData == null) return; 
        upgradeFeaturesDetails = _buildingData.GetUpgradeFeaturesDetails();

        //if (upgradeFeaturesDetails == null) return;
        for (int i = 0; i < _features.Length; i++)
        {
            for (int j = 0; j < upgradeFeaturesDetails.featuresDetails.Length; j++)
            {
                if (_features[i].feature == upgradeFeaturesDetails.featuresDetails[j].feature)
                {
                    _features[i].gameObject.SetActive(true);
                    _features[i].GetComponent<SliderController>().UpdateData(upgradeFeaturesDetails.featuresDetails[i]);
                    break;
                }
            }
        }
    }
}
