using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingData : MonoBehaviour
{
    #region Adjustable data
    [Header("Data Container Info")]
    [SerializeField] protected bool _placeableModel = false;
    [SerializeField] protected bool _placing = false;

    [Header("Size")]
    [SerializeField] protected int _width = 2; // The x-axis
    [SerializeField] protected int _length = 2; // The z-axis

    [Header("Location")]
    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;

    [Header("Model Data")]
    [SerializeField] private string _modelPath = "";
    [SerializeField] private GameObject _model = null;
    private MeshRenderer[] _childremMeshRenderers;
    [SerializeField] private BuildingType _buildingType;
    public BuildingType BuildingType { get { return _buildingType; } }
    
    [Header("Interactions")]
    [SerializeField] private InputActionReference _removeAction;
    #endregion

    internal int X { get { return _x; } private set {  } }
    internal int Z { get { return _z; } private set {  } }

    // The x and z might cause the bulding to be out of range
    // therefore, the applied x and z will hold the value of
    // _x and _z after limiting the values to ensure the building 
    // being inside of the boundries of the map
    private int appliedX = 0; 
    private int appliedZ = 0;


    private ListItemData _listData; // Used to return the object to be reorganized. 
    private bool _removed = false;

    public TextMeshProUGUI xInfo;
    public TextMeshProUGUI zInfo;

    private string _townDataTag = "TownData";
    private TownData _townData;
    
    protected TownData _TownData { 
        get
        {
            TownData townData = _townData;

            if(townData == null)
                _townData = GameObject.FindGameObjectWithTag(_townDataTag).GetComponent<TownData>();

            return townData;
        }
    }

    private ColorChangeHoverInteractor _interactor;


    private List<Action<int, int>> _locationUpdateCallbacksList = new List<Action<int, int>>(); 


    // Start is called before the first frame update
    protected void Start()
    {
        // Get the town data
        _townData = GameObject.FindGameObjectWithTag(_townDataTag).GetComponent<TownData>();
        if(_townData == null)
        { // Handle error
            throw new System.Exception("The TownData does not exists in this scene or is inactive!");
        }

        if(_removeAction != null)
        {
            _removeAction.action.performed += _RemoveBuilding;
            _listData = ListItemData.lastInteractedItemListData;
        }
        _interactor = GetComponent<ColorChangeHoverInteractor>();

        // Update the x, y position
        UpdateLocation(_x, _z); // Ensure that the other callbacks that are listnening for the position are notified of the start position

        retriveData();
        _SyncPosition();
    }

    // Update is called once per frame
    void Update()
    {
        // Get access to the mouse position
        if(_placing && _placeableModel)
        {
            //print("Positning");
            //AssignPosition(GroundBlock.X, GroundBlock.Z);
            //MoveDemo(GroundBlock.X, GroundBlock.Z);
        }
    }

    private void _RemoveBuilding(InputAction.CallbackContext context)
    {
        if(!context.canceled && !_removed && _interactor.hovered)
        {
            _listData.Increase();
            _removed = true;
            Destroy(gameObject, 0f);
        }
    }


    public void MoveDemo(int x, int z)
    {
        if (_placing && _placeableModel)
        {
            AssignPosition(x, z);

            _CheckCollision(x, z);
        }
    }


    // Getter
    public bool PlaceModel(int x, int z)
    {
        if(_model == null) _model = Resources.Load<GameObject>(_modelPath);


        // Check if there is any collision
        if(_CheckCollision(x, z))
        {
            // Play collision sound
            print("Building can not be placed here");
            return false;
        }

        // Create the model, the data will be synced automatically
        BuildingData modelData = Instantiate(_model).GetComponent<BuildingData>();

        modelData.AssignX(x);
        modelData.AssignZ(z);

        // Assign the model to the ground areas it will occupy
        _AssignBuildingToGroundBlock(modelData);


        _OverrideModelData(modelData.gameObject);

        // Try to add a new demo of the same type as this one
        DemoAdder.LastDemoAdded.AddDelayedDemo(GroundBlock.demo, InteractionsData.addDemoDelayInSeconds);


        // Destroy the demo
        GroundBlock.demo = null;
        gameObject.SetActive(false);
        Destroy(gameObject, InteractionsData.addDemoDelayInSeconds + 0.1f);

        return true;
    }

    private void _AssignBuildingToGroundBlock(BuildingData modelData)
    {
        // Assign the model to the ground areas it will occupy
        int startX = appliedX - (int)(_width / 2f);
        int endX = appliedX + (int)(_width / 2f);
        int startZ = appliedZ - (int)(_length / 2f);
        int endZ = appliedZ + (int)(_length / 2f);

        for (int row = startX; row <= endX; row++)
        {
            for (int col = startZ; col <= endZ; col++)
            {
                GroundData.GetGroundBlock(row, col).SetBuilding(modelData);
            }
        }
    }

    private bool _CheckCollision(int x, int z)
    {
        bool collieded = false; ;

        // Get the start and the end positions of the model
        int startX = appliedX - (int)(_width / 2f);
        int endX = appliedX + (int)(_width / 2f);
        int startZ = appliedZ - (int)(_length / 2f);
        int endZ = appliedZ + (int)(_length / 2f);

        //print($"X: ({startX}, {endX}), Z: ({startZ}, {endZ}), Applied: ({appliedX}, {appliedZ})");

        GroundBlock block;
        for(int row = startX; row <= endX; row++)
        {
            for(int col = startZ; col <= endZ; col++)
            {
                block = GroundData.GetGroundBlock(row, col);
                if(block.IsOccupied())
                {
                    block.IndicateOccupiedGround();
                    collieded = true;
                }
                else
                {
                    block.IndicateUnoccupiedGround();
                }
            }
        }

        // Check if the ground block(s) are already occupied
        //block = GroundData.GetGroundBlock(x, z);
        //if (block.IsOccupied())
        //{ // Indicate that this block is already occupied
        //    block.IndicateOccupiedGround();
        //    collieded = true;
        //}

        return collieded;
    }


    // Setters
    public void AssignPosition(int x, int z)
    {
        UpdateLocation(x, z);

        _SyncPosition();
    }


    internal void UpdateLocation(int x, int z)
    {
        AssignX(x);
        AssignZ(z);

        // Notify the other locations
        for(int i = 0; i < _locationUpdateCallbacksList.Count; i++)
        {
            _locationUpdateCallbacksList[i](x, z);
        }
    }

    internal void AssignX(int x)
    {
        _x = x;
        //xInfo.text = "x: " + x;
    }

    internal void AssignZ(int z)
    {
        _z = z;
        //zInfo.text = "z: " + z;
    }

    private void _SyncPosition()
    {
        //print(transform.lossyScale.y / 2);
        // Get the center position of the ground
        float groundX = _TownData.transform.position.x;
        float groundZ = _TownData.transform.position.z;

        // Get the width and height of the ground
        int groundWidth = GroundData.width;
        int groundLength = GroundData.length;

        

        // Check if the position is out of boundries
        if(_x < (-groundWidth + (int)(_width / 2f)))
        {
            appliedX = -groundWidth + (int)(_width/2f); // Move back to boundries
        }
        else if(_x > (groundWidth - (int)(_width / 2f)))
        {
            appliedX = groundWidth - (int)(_width / 2f); // Move back to boundries
        }
        else
        {
            appliedX = _x;
        }

        if (_z < (-groundLength + (int)(_length / 2f)))
        {
            appliedZ = -groundLength + (int)(_length / 2f); // Move back to boundries
        }
        else if (_z > (groundLength - (int)(_length / 2f)))
        {
            appliedZ = groundLength - (int)(_length / 2f); // Move back to boundries
        }
        else
        {
            appliedZ = _z;
        }

        if(xInfo != null & zInfo != null)
        {
            xInfo.text = "x: " + appliedX;
            zInfo.text = "z: " + appliedZ;
        }


        // Move the building to the center of the defined position
        transform.position = new Vector3(
            groundX /* - groundWidth */ + appliedX,
            0.1f /*transform.lossyScale.y/2 + _townData.transform.position.y + _townData.transform.lossyScale.y/2*/,
            groundZ /* - groundHeight */ + appliedZ
        );
    }


    internal void RegisterLocationUpdateCallback(Action<int, int> callback)
    {
        _locationUpdateCallbacksList.Add( callback );
    }

    internal bool isDemo()
    {
        return _placing && _placeableModel;
    }


    /// <summary>
    /// This method is called when the model is being placed.
    /// </summary>
    /// <param name="gameObject">The new model that has been initialized.</param>
    protected virtual void _OverrideModelData(GameObject gameObject)
    { // This purpose of this method is to allow the sub-classes to override the model data before it is added.

    }

    // Retrive the project info
    private void retriveData()
    {
        // Retrive and assign the data
    }
}

