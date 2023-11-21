using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListItemData : MonoBehaviour
{
    [SerializeField] private int _count = 1;
    [SerializeField] private BuildingType _buildingType;
    [SerializeField] private TextMeshProUGUI _counter;

    private static ListItemData _lastInteractedItemListData;

    // Start is called before the first frame update
    void Start()
    {
        _UpdateCounterUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Decrease()
    {
        if (_count <= 0) return;

        _count--;
        _UpdateCounterUI();

        // Register this as the last interacted item list, this will allow for returning the building
        // without placing it on the ground.
        _lastInteractedItemListData = this;

        if (_count <= 0)
        {
            //gameObject.SetActive(false); // TODO: This should be added later on?
        }
    }

    private void _Increase()
    {
        _count++;
        _UpdateCounterUI();
    }

    public bool IsInStock()
    {
        return _count > 0;
    }

    public string GetBuildingType()
    {
        return _buildingType.ToString();
    }

    public static void CancelBuildingPlacement()
    {
        if( _lastInteractedItemListData != null )
        {
            _lastInteractedItemListData._Increase();
        }
    }

    #region UI

    private void _UpdateCounterUI()
    {
        _counter.text = _count.ToString();
    }

    #endregion
}
