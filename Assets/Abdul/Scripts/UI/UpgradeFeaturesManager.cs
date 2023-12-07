using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeFeature
{
    Health,
    GoldStorage,
    SoliderCapacity,
    Damage,
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


    //private IEnumerator trying()
    //{
    //    //yield return new WaitUntil()
    //}

    private void OnEnable()
    {
        if (_buildingData == null) return; 
        upgradeFeaturesDetails = _buildingData.GetUpgradeFeaturesDetails();

        //if (upgradeFeaturesDetails == null) return;
        for (int featureI = 0; featureI < _features.Length; featureI++)
        {
            for (int detailI = 0; detailI < upgradeFeaturesDetails.featuresDetails.Length; detailI++)
            {
                if (_features[featureI].feature == upgradeFeaturesDetails.featuresDetails[detailI].feature)
                {
                    _features[featureI].gameObject.SetActive(true);
                    _features[featureI].GetComponent<SliderController>().UpdateData(upgradeFeaturesDetails.featuresDetails[detailI]);
                    break;
                }
            }
        }
    }
}
