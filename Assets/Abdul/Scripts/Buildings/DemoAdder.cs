using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAdder : MonoBehaviour
{
    [Header("Sounds Settings")]
    [SerializeField] private string _demoAddedSound = "Confirmation";
    [SerializeField] private string _buildingOutOfStockSound = "denied";


    private static string _demosPath = "Town/Demos/";
    [SerializeField] private string _buildingName;
    private ListItemData _listData;

    internal static DemoAdder LastDemoAdded { get; private set; }

    private void Start()
    {
        _listData = GetComponent<ListItemData>();
    }

    public void AddDemo()
    {
        if(!_listData.IsInStock())
        { // There is no more buildings of this type to place

            // Play a notifying sound to indicate that there is no more buildings of this type to locate
            SoundManager.Instance.PlayActionSound(_buildingOutOfStockSound);
            return;
        }


        // Check if the user is already has a building in the placement process.
        _HandleDemoReplace();

        // Get the demo
        GameObject demo = Resources.Load<GameObject>(_demosPath + _buildingName);

        if (demo == null)
        {
            throw new System.Exception("The demo with the name (" + 
                _buildingName + ") deos not exists inside the folder (" + _demosPath + " )!");
        }
        GroundBlock.demo = Instantiate(demo);
        //GroundBlock.demo.GetComponent<BuildingData>().PlaceModel(Random.Range(0, 5), Random.Range(0, 5));

        // Assign the indestructible to the demo
        DemoData demoData = GroundBlock.demo.GetComponent<DemoData>();
        demoData.AssignIndestructible(_listData.Decrease());


        // Register this as the last adder that added a demo
        LastDemoAdded = this;

        // Play a sound that indicates that the demo has been added
        SoundManager.Instance.PlayActionSound(_demoAddedSound);


        // Check if this is the last demo of this type
        if(!_listData.IsInStock())
        {
            Destroy(gameObject, 0f); // TODO: Maybe this should be re-added later on?
        }

    }


    public void AddDelayedDemo(GameObject objectToCopy, float delayInSeconds)
    {
        StartCoroutine(_AddDemo(objectToCopy, delayInSeconds));
    }

    private IEnumerator _AddDemo(GameObject objectToCopy, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        AddDemo();

        // Copy the rotation, position info
        _CopyInfo(objectToCopy, GroundBlock.demo);
    }

    private void _CopyInfo(GameObject objectToCopy, GameObject demo)
    {
        if (objectToCopy != null && demo != null)
        {
            _CopyPositionInfo(objectToCopy, demo);

            _CopyRotationInfo(objectToCopy, demo);
        }
    }

    private void _CopyPositionInfo(GameObject objectToCopy, GameObject demo)
    {
        DemoData dataToCopy = objectToCopy.GetComponent<DemoData>();
        DemoData demoData;
        demo.TryGetComponent(out demoData);


        demoData.UpdateLocation(dataToCopy.X, dataToCopy.Z);
    }

    private void _CopyRotationInfo(GameObject objectToCopy, GameObject demo)
    {
        RotateableBuilding rotateableBuildingToCopy = objectToCopy.GetComponent<RotateableBuilding>();
        RotateableBuilding demoRotateable = demo.GetComponent<RotateableBuilding>();

        if (rotateableBuildingToCopy != null && demoRotateable != null)
        {
            demoRotateable.SetRotation(rotateableBuildingToCopy.RotatedAmount);
        }
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
