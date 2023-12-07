using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoData : MonoBehaviour
{
    [Header("Sounds Data")]
    [SerializeField] private string _demoAddedSoundName = "PutDown";
    [SerializeField] private string _areaOcuppiedSoundName = "denied";

    [Header("Model Data")]
    [SerializeField] protected int _level = 1;
    [SerializeField] private string _modelPath = "";
    [SerializeField] private GameObject _model = null;
    [SerializeField] private Indestructible _indestructible;

    [Header("Size")]
    [SerializeField] protected int _width = 2; // The x-axis
    [SerializeField] protected int _length = 2; // The z-axis

    [Header("Location")]
    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;
    internal int X { get { return _x; } private set { } }
    internal int Z { get { return _z; } private set { } }

    // The x and z might cause the bulding to be out of range
    // therefore, the applied x and z will hold the value of
    // _x and _z after limiting the values to ensure the building 
    // being inside of the boundries of the map
    protected int appliedX = 0;
    protected int appliedZ = 0;


    private string _townDataTag = "TownData";
    private TownData _townData;

    protected TownData _TownData
    {
        get
        {
            TownData townData = _townData;

            if (townData == null)
            {
                _townData = GameObject.FindGameObjectWithTag(_townDataTag).GetComponent<TownData>();
                townData = _townData;
            }

            return townData;
        }
    }

    private List<Action<int, int>> _locationUpdateCallbacksList = new List<Action<int, int>>();

    internal void RegisterLocationUpdateCallback(Action<int, int> callback)
    {
        _locationUpdateCallbacksList.Add(callback);
    }


    protected void Start()
    {
        
    }

    public void MoveDemo(int x, int z)
    {
        AssignPosition(x, z);
        _CheckCollision(x, z);
    }

    public bool PlaceModel(int x, int z)
    {
        if (_model == null) _model = Resources.Load<GameObject>(_modelPath);


        // Check if there is any collision
        if (_CheckCollision(x, z))
        {
            // Play collision sound
            SoundManager.Instance.PlayActionSound(_areaOcuppiedSoundName);
            return false;
        }

        // Create the model, the data will be synced automatically
        IDableBuilding modelData = Instantiate(_model).GetComponent<IDableBuilding>();

        modelData.AssignX(x);
        modelData.AssignZ(z);

        // Assign the indestructible data to the building
        modelData.AssignIndestructible(_indestructible);

        // Assign the model to the ground areas it will occupy
        _AssignBuildingToGroundBlock(modelData);


        // Add later!!
        _OverrideModelData(modelData.gameObject);

        // Try to add a new demo of the same type as this one
        DemoAdder.LastDemoAdded.AddDelayedDemo(GroundBlock.demo, InteractionsData.addDemoDelayInSeconds);


        // Play the sound of the building being put down // To confirm the action
        SoundManager.Instance.PlayActionSound(_demoAddedSoundName);

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
        for (int row = startX; row <= endX; row++)
        {
            for (int col = startZ; col <= endZ; col++)
            {
                block = GroundData.GetGroundBlock(row, col);
                if (block.IsOccupied())
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
        for (int i = 0; i < _locationUpdateCallbacksList.Count; i++)
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
        if (_x < (-groundWidth + (int)(_width / 2f)))
        {
            appliedX = -groundWidth + (int)(_width / 2f); // Move back to boundries
        }
        else if (_x > (groundWidth - (int)(_width / 2f)))
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


        // Move the building to the center of the defined position
        transform.position = new Vector3(
            groundX /* - groundWidth */ + appliedX,
            0.1f /*transform.lossyScale.y/2 + _townData.transform.position.y + _townData.transform.lossyScale.y/2*/,
            groundZ /* - groundHeight */ + appliedZ
        );
    }


    internal void AssignIndestructible(Indestructible indestructible)
    {
        _indestructible = indestructible;
    }

    /// <summary>
    /// This method is called when the model is being placed.
    /// </summary>
    /// <param name="gameObject">The new model that has been initialized.</param>
    internal virtual void _OverrideModelData(GameObject gameObject)
    { // This purpose of this method is to allow the sub-classes to override the model data before it is added.

    }
}
