using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ResourcesType
{
    Gold,
    Elixir,
}

public class UpgradeBuildingInfo : MonoBehaviour
{
    [SerializeField] private int _cost = 25;
    [SerializeField] private int _timeRequiredInSeconds = 10;
    [SerializeField] private int _updateRateInSeconds = 1;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private ResourcesType _resourcesType;
    [SerializeField] private SliderBarsController _resourcesStorage;
    [SerializeField] private bool _upgrade;

    private int _constructionTimeProgress = 0;

    // Start is called before the first frame update
    void Start()
    {
        switch(_resourcesType)
        {
            case ResourcesType.Gold:
                _resourcesStorage = GoldBank.instance;
                break;
            case ResourcesType.Elixir:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade()
    {
        // Check if there is enough money
        if(!_upgrade && _resourcesStorage.HasResources(_cost))
        {
            _resourcesStorage.IncreaseDecreaseValue(-_cost);
            StartCoroutine(_Upgrade());    
        }
    }


    private IEnumerator _Upgrade()
    {
        _upgrade = true;
        int nextUpdateTime = _updateRateInSeconds;

        if(_constructionTimeProgress + nextUpdateTime > _timeRequiredInSeconds)
        {
            nextUpdateTime = _timeRequiredInSeconds - _constructionTimeProgress;
        }

        _constructionTimeProgress += nextUpdateTime;

        yield return new WaitForSeconds(nextUpdateTime);

        if(_constructionTimeProgress < _timeRequiredInSeconds)
        {
            _Upgrade();
        }
        else
        {
            _upgrade = false;
        }
    }
}
