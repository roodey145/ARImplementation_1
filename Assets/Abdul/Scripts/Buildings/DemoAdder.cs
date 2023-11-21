using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAdder : MonoBehaviour
{
    private static string _demosPath = "Town/Demos/";
    [SerializeField] private string _buildingName;
    private ListItemData _listData;

    private void Start()
    {
        _listData = GetComponent<ListItemData>();
    }

    public void AddDemo()
    {
        if(!_listData.IsInStock())
        { // There is no more buildings of this type to place

            // TODO: Play a notifying sound to indicate that there is no more buildings of this type to locate
            print($"No More Buildings of type ({_listData.GetBuildingType()})");
            return;
        }


        // Check if the user is already has a building in the placement process.
        _HandleDemoReplace();

        // Get the demo
        GameObject demo = Resources.Load<GameObject>(_demosPath + _buildingName);
        if(demo == null)
        {
            throw new System.Exception("The demo with the name (" + 
                _buildingName + ") deos not exists inside the folder (" + _demosPath + " )!");
        }
        GroundBlock.demo = Instantiate(demo);
        //GroundBlock.demo.GetComponent<BuildingData>().PlaceModel(Random.Range(0, 5), Random.Range(0, 5));

        _listData.Decrease();

        //Destroy(gameObject, 0f); // TODO: Maybe this should be re-added later on?
    }


    private void _HandleDemoReplace()
    {
        if(GroundBlock.demo != null)
        { // There is a demo that should be returned to the UI
            ListItemData.CancelBuildingPlacement();

            // Destroy the current demo
            Destroy(GroundBlock.demo);
        }
    }
}
