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
    //[Header("Data Container Info")]
    //[SerializeField] protected bool _placeableModel = false;
    //[SerializeField] protected bool _placing = false;

    [Header("Audio Settings")]
    [SerializeField] protected string _levelUpClipName = "Level up";

    [Header("Size")]
    [SerializeField] protected int _width = 2; // The x-axis
    [SerializeField] protected int _length = 2; // The z-axis

    [Header("Location")]
    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;

    [Header("Model Data")]
    [SerializeField] protected int _level = 1;
    [SerializeField] private string _modelPath = "";
    [SerializeField] private GameObject _model = null;
    private MeshRenderer[] _childremMeshRenderers;
    [SerializeField] protected BuildingType _buildingType;
    public BuildingType BuildingType { get { return _buildingType; } }
    #endregion

    internal int X { get { return _x; } private set {  } }
    internal int Z { get { return _z; } private set {  } }

    // The x and z might cause the bulding to be out of range
    // therefore, the applied x and z will hold the value of
    // _x and _z after limiting the values to ensure the building 
    // being inside of the boundries of the map
    protected int appliedX = 0;
    protected int appliedZ = 0;


    protected ListItemData _listData; // Used to return the object to be reorganized. 

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

    private List<Action<int, int>> _locationUpdateCallbacksList = new List<Action<int, int>>(); 
    private List<Action<int>> _levelUpdateCallbacksList = new List<Action<int>>();

    protected void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected void Start()
    {
        // Get the town data
        _townData = GameObject.FindGameObjectWithTag(_townDataTag).GetComponent<TownData>();
        if(_townData == null)
        { // Handle error
            throw new System.Exception("The TownData does not exists in this scene or is inactive!");
        }

        
        _listData = ListItemData.lastInteractedItemListData;


        // Update the x, y position
        UpdateLocation(_x, _z); // Ensure that the other callbacks that are listnening for the position are notified of the start position

        retriveData();
        _SyncPosition();
    }

    // Update is called once per frame
    void Update()
    {

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
        _locationUpdateCallbacksList.Add(callback);
    }

    internal void RegisterLevelUpdateCallback(Action<int> callback)
    {
        _levelUpdateCallbacksList.Add(callback);
    }

    //internal bool isDemo()
    //{
    //    return _placing && _placeableModel;
    //}

    internal virtual void LevelUp()
    {
        _level++;
        for(int i = 0; i < _levelUpdateCallbacksList.Count;  i++)
        {
            _levelUpdateCallbacksList[i](_level);
        }

        // Play level up sound
        SoundManager.Instance.PlayActionSound(_levelUpClipName);
    }

    internal void AssignLLevel(int level)
    {
        _level = level;
        for (int i = 0; i < _levelUpdateCallbacksList.Count; i++)
        {
            _levelUpdateCallbacksList[i](_level);
        }
    }

    internal int GetLevel()
    {
        return _level;
    }


    /// <summary>
    /// This method is called when the model is being placed.
    /// </summary>
    /// <param name="gameObject">The new model that has been initialized.</param>
    //internal virtual void _OverrideModelData(GameObject gameObject)
    //{ // This purpose of this method is to allow the sub-classes to override the model data before it is added.

    //}

    // Retrive the project info
    private void retriveData()
    {
        // Retrive and assign the data
    }
}

