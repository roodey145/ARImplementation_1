using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ListItemData : MonoBehaviour
{
    [SerializeField] private int _count = 1;
    [SerializeField] private Queue<Indestructible> _items = new Queue<Indestructible>();
    [SerializeField] internal BuildingType _buildingType;
    [SerializeField] private TextMeshProUGUI _counter;

    internal static ListItemData lastInteractedItemListData { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _UpdateCounterUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Indestructible Decrease()
    {
        _count--;
        _UpdateCounterUI();

        // Register this as the last interacted item list, this will allow for returning the building
        // without placing it on the ground.
        lastInteractedItemListData = this;

        if (_count <= 0)
        {
            // Remove this from the list and from the list items manager
            //gameObject.SetActive(false); // TODO: This should be added later on?
        }
        return _items.Dequeue();
    }

    internal void AddListItem(Indestructible item)
    {
        _items.Enqueue(item);
        _count = _items.Count;
        _UpdateCounterUI();
    }

    internal void Increase()
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
        if( lastInteractedItemListData != null )
        {
            lastInteractedItemListData.AddListItem(GroundBlock.demo.GetComponent<DemoData>().Indestructible);
        }
        else
        {
            ListItemsManager.instance.ReturnBuilding(GroundBlock.demo.GetComponent<DemoData>().Indestructible);
        }
    }

    #region UI

    private void _UpdateCounterUI()
    {
        _counter.text = _count.ToString();
    }

    #endregion
}
