using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public enum ResourcesType
{
    Gold,
    Elixir,
}

public class UpgradeBuildingInfo : MonoBehaviour
{
    //[SerializeField] private int _cost = 25;
    //[SerializeField] private int _timeRequiredInSeconds = 10;
    //[SerializeField] private int _updateRateInSeconds = 1;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private SliderBarsController _progressSlider;
    [SerializeField] private ResourcesType _resourcesType;
    [SerializeField] private bool _upgrade;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Color _textColorIfHasResources = Color.green;
    [SerializeField] private Color _textColorIfNoResources = Color.red;

    [SerializeField] private UpgradeableBuildingData _buildingData;

    private int _constructionTimeProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get the slider bar controller if it is null
        if (_progressSlider == null)
        {
            _progressSlider = GetComponentInChildren<SliderBarsController>();
        }

        // Get access to the building data
        _buildingData = GetComponentInParent<UpgradeableBuildingData>();

        // Set up the progress bar
        _SetUpProgressBar();

        // Change the color of the text depeing on whether there is enough money or not
        UpdatePriceColor();


        // Register the show/hide on hover and hoverExit respectively
        _buildingData.RegisterHoverCallback(_ShowMenu);
        _buildingData.RegisterHoverExitCallback(_HideMenu);

        // Hide the menu and the timer
        _HideData();
    }

    private void _SetUpProgressBar()
    {
        // Set the capacity of the progress slider
        _progressSlider.SetCapacity(_buildingData.GetUpgradeTimeInSeconds());
        _progressSlider.SetValue(0);
    }

    internal void UpdatePriceColor()
    {
        print("Building Data:" + _buildingData);
        print("Bank Data: " + _buildingData.GetBank());
        print("Cost: " + _buildingData.GetCost());

        if (_buildingData.GetBank().HasResources(_buildingData.GetCost()))
        {
            _priceText.color = _textColorIfHasResources;
            _priceText.text = $"<color=#{_textColorIfHasResources.ToHexString()}>{_buildingData.GetCost()} {_resourcesType.ToString().ToUpper()[0]}</color>";
        }
        else
        {
            _priceText.color = _textColorIfNoResources;
            _priceText.text = $"<color=#{_textColorIfNoResources.ToHexString()}>{_buildingData.GetCost()} {_resourcesType.ToString().ToUpper()[0]}</color>";
        }
    }

    private void _HideData()
    {
        _HideMenu();
        _ResetProgressBar();
    }

    public void Upgrade()
    {
        // Check if there is enough money
        if(!_upgrade && _buildingData.GetBank().HasResources(_buildingData.GetCost()))
        {
            _buildingData.GetBank().IncreaseDecreaseValue(-_buildingData.GetCost());

            // Hide the upgrade menu and show the progress bar
            _upgradeMenu.SetActive(false);
            _progressSlider.gameObject.SetActive(true);
            StartCoroutine(_Upgrade());    
        }
    }


    private IEnumerator _Upgrade()
    {
        _upgrade = true;
        int nextUpdateTime = _buildingData.GetUpdateRateInSeconds();

        if(_constructionTimeProgress + nextUpdateTime > _buildingData.GetUpgradeTimeInSeconds())
        {
            nextUpdateTime = _buildingData.GetUpgradeTimeInSeconds() - _constructionTimeProgress;
        }

        _constructionTimeProgress += nextUpdateTime;

        yield return new WaitForSeconds(nextUpdateTime);
        
        // Increase the progress bar time
        _progressSlider.IncreaseDecreaseValue(nextUpdateTime);


        if(_constructionTimeProgress < _buildingData.GetUpgradeTimeInSeconds())
        {
            StartCoroutine(_Upgrade());
        }
        else
        {
            _upgrade = false;
            _constructionTimeProgress = 0;
            _ResetProgressBar();
            _buildingData.Upgrade();
            _SetUpProgressBar();
            // Change the color of the text depeing on whether there is enough money or not
            UpdatePriceColor();
        }
        //print("Progress Time: " + _constructionTimeProgress);
    }

    // Show the menu
    private void _ShowMenu()
    {
        //if(_upgrade)
        //{
        //    _progressSlider.gameObject.SetActive(true);
        //}
        //else
        //{
        //    UpdatePriceColor();
        //    _upgradeMenu.SetActive(true);
        //}

        if( !_upgrade )
        {
            UpdatePriceColor();
            _upgradeMenu.SetActive(true);
        }
    }

    private void _HideMenu()
    {
        //_progressSlider.gameObject.SetActive(false);
        _upgradeMenu.SetActive(false);
    }

    private void _ResetProgressBar()
    {
        _progressSlider.SetValue(0);
        _progressSlider.gameObject.SetActive(false);
    }
}
