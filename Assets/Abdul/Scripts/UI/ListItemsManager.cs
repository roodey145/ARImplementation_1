using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ListItemsManager : MonoBehaviour
{
    public static ListItemsManager instance;
    [SerializeField] private string _listItemsPath = "UI/ListItems/";
    [SerializeField] private ManagerListItemData[] _listItems;
    private Dictionary<BuildingType, Dictionary<int, ListItemData>> listItemsData = new Dictionary<BuildingType, Dictionary<int, ListItemData>>();
    public bool organizingBase = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (organizingBase)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    internal bool IsAllBuildingsAdded()
    {
        bool buildingAdded = true;
        foreach (Dictionary<int, ListItemData> item in listItemsData.Values)
        {
            foreach(ListItemData itemData in item.Values)
            {
                if(itemData.IsInStock())
                {
                    buildingAdded = false;
                    break;
                }
            }

            if(!buildingAdded)
            { // It was proved that there are still buildings left to add
                break;
            }
        }

        return buildingAdded;
    }


    internal void ReturnBuilding(Indestructible buildingData)
    {
        // Get the Dictionary of the specified building
        ListItemData storePlace = _GetIndestructibleStore(buildingData);
        
        // Make sure to create a ListItemData to store buildings of this type and level
        if(storePlace == null)
        {
            storePlace = _CreateStorePlace(buildingData);
        }

        // Store the building data in the corrosponding ListItemData
        storePlace.AddListItem(buildingData);
    }

    private ListItemData _GetIndestructibleStore(Indestructible indestructible)
    {
        ListItemData storePlace = null;

        // Get the dictionary which stores the pair of building level and indestructible data that matches this building type
        Dictionary<int, ListItemData> store;
        listItemsData.TryGetValue(indestructible.type, out store);

        // Try to get the list of that specific level of this building
        if (store != null)
            store.TryGetValue(indestructible.level, out storePlace);


        return storePlace;
    }

    private ListItemData _CreateStorePlace(Indestructible indestructible)
    {
        ListItemData storePlace;
        // Get the dictionary which stores the pair of building level and indestructible data that matches this building type
        Dictionary<int, ListItemData> store;
        listItemsData.TryGetValue(indestructible.type, out store);

        if(store == null)
        {
            // Create the building levels info store
            store = new Dictionary<int, ListItemData>();
            listItemsData.Add(indestructible.type, store);
        }

        store.TryGetValue(indestructible.level, out storePlace);

        if(storePlace == null)
        {
            // Create itemlist
            for (int i = 0; i < _listItems.Length; i++)
            {
                if (_listItems[i].buildingType == indestructible.type)
                {
                    print(_listItemsPath + _listItems[i].UIItemListPath[indestructible.level - 1]);
                    GameObject listItemModel = Resources.Load<GameObject>(_listItemsPath + _listItems[i].UIItemListPath[indestructible.level - 1]); ;
                    // Create the specifc level store place
                    GameObject model = Instantiate(listItemModel, transform);
                    storePlace = model.GetComponent<ListItemData>();
                    break;
                }
            }

            // Make sure to destory the UI if there is a building with the same level but is null.
            store.Remove(indestructible.level);

            // Add the store place to the dictionary
            store.Add(indestructible.level, storePlace);
            print("KEY: " + indestructible.level + ", " + storePlace);
        }

        return storePlace;
    }

    private void _CreateUI(Indestructible indestructible)
    {

    }
}
