using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    [Header("Data Container Info")]
    [SerializeField] private bool _placeableModel = false;
    [SerializeField] private bool _placing = false;

    [Header("Size")]
    [SerializeField] private int _width = 2;
    [SerializeField] private int _height = 2;

    [Header("Location")]
    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;


    [Header("Model Data")]
    [SerializeField] private string _modelPath = "";
    [SerializeField] private GameObject _model = null;


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

        if(_placeableModel)
        {
            _SyncPosition();
        }

        retriveData();
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
    public void InitializeModelDemo()
    {
        _model = Resources.Load<GameObject>(_modelPath);
        _placing = true;
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
    }

    public void _AssignZ(int z)
    {
        _z = z;
    }



    private void _SyncPosition()
    {
        // Get the center position of the ground
        float groundX = _townData.transform.position.x;
        float groundZ = _townData.transform.position.z;

        // Get the width and height of the ground
        float groundWidth = _townData.Width();
        float groundHeight = _townData.Height();


        // Move the building to the center of the defined position
        transform.position = new Vector3(
            groundX - groundWidth + _x,
            transform.lossyScale.y/2 + _townData.transform.position.y + _townData.transform.lossyScale.y/2,
            groundZ - groundHeight + _z
        );
    }

    // Retrive the project info
    private void retriveData()
    {
        // Retrive and assign the data
    }
}
