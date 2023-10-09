using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    [Header("Data Container Info")]
    [SerializeField] private bool _placeableModel = false;
    [SerializeField] private bool _placing = false;

    [Header("Size")]
    [SerializeField] private int _width = 2; // The x-axis
    [SerializeField] private int _length = 2; // The z-axis

    [Header("Location")]
    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;

    // The x and z might cause the bulding to be out of range
    // therefore, the applied x and z will hold the value of
    // _x and _z after limiting the values to ensure the building 
    // being inside of the boundries of the map
    private int appliedX = 0; 
    private int appliedZ = 0;


    [Header("Model Data")]
    [SerializeField] private string _modelPath = "";
    [SerializeField] private GameObject _model = null;


    public TextMeshProUGUI xInfo;
    public TextMeshProUGUI zInfo;

    private string _townDataTag = "TownData";
    private TownData _townData;
    // Start is called before the first frame update
    void Start()
    {
        // Get the town data
        _townData = GameObject.FindGameObjectWithTag(_townDataTag).GetComponent<TownData>();
        if(_townData == null)
        { // Handle error
            throw new System.Exception("The TownData does not exists in this scene or is inactive!");
        }

        //if(_placeableModel)
        //{
        //}

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
            AssignPosition(GroundBlock.X, GroundBlock.Z);
        }
    }



    // Getter
    public void PlaceModel(int x, int z)
    {
        if(_model == null) _model = Resources.Load<GameObject>(_modelPath);


        // Create the model, the data will be synced automatically
        BuildingData modelData = Instantiate(_model).GetComponent<BuildingData>();

        modelData._AssignX(x);
        modelData._AssignZ(z);

        // Destroy the demo
        GroundBlock.demo = null;
        Destroy(gameObject, 0f);

        //_placing = true;
    }



    // Setters
    public void AssignPosition(int x, int y)
    {
        _AssignX(x);
        _AssignZ(y);

        _SyncPosition();
    }


    private void _AssignX(int x)
    {
        _x = x;
        //xInfo.text = "x: " + x;
    }

    public void _AssignZ(int z)
    {
        _z = z;
        //zInfo.text = "z: " + z;
    }



    private void _SyncPosition()
    {
        //print(transform.lossyScale.y / 2);
        // Get the center position of the ground
        float groundX = _townData.transform.position.x;
        float groundZ = _townData.transform.position.z;

        // Get the width and height of the ground
        int groundWidth = _townData.Width();
        int groundLength = _townData.Length();

        

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

    // Retrive the project info
    private void retriveData()
    {
        // Retrive and assign the data
    }
}
