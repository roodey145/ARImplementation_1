using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locateable : MonoBehaviour
{
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


    public void MoveDemo(int x, int z)
    {
        AssignPosition(x, z);
        CheckCollision(x, z);
    }

    /// <summary>
    /// Checks if the specified location is occupied by a building.
    /// </summary>
    /// <param name="x">The x location of the block.</param>
    /// <param name="z">The z location of the block.</param>
    /// <returns>True if the specified location is ocupied, false otherwise.</returns>
    internal bool CheckCollision(int x, int z)
    {
        bool collieded = false; ;

        // Get the start and the end positions of the model
        int startX = x - (int)(_width / 2f);
        int endX = x + (int)(_width / 2f);
        int startZ = z - (int)(_length / 2f);
        int endZ = z + (int)(_length / 2f);

        //print($"X: ({startX}, {endX}), Z: ({startZ}, {endZ}), Applied: ({appliedX}, {appliedZ})");

        GroundBlock block;
        for (int row = startX; row <= endX; row++)
        {
            for (int col = startZ; col <= endZ; col++)
            {
                block = GroundData.GetGroundBlock(row, col);
                if (block.IsOccupied())
                {
                    //block.IndicateOccupiedGround();
                    collieded = true;
                }
                //else
                //{
                //    block.IndicateUnoccupiedGround();
                //}
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
            0 /*transform.lossyScale.y/2 + _townData.transform.position.y + _townData.transform.lossyScale.y/2*/,
            groundZ /* - groundHeight */ + appliedZ
        );
    }


    public Vector3 GetPosition(int x, int z)
    {
        //print(transform.lossyScale.y / 2);
        // Get the center position of the ground
        float groundX = _TownData.transform.position.x;
        float groundZ = _TownData.transform.position.z;



        // Move the building to the center of the defined position
        return new Vector3(
            groundX /* - groundWidth */ + x,
            0 /*transform.lossyScale.y/2 + _townData.transform.position.y + _townData.transform.lossyScale.y/2*/,
            groundZ /* - groundHeight */ + z
        );
    }
}
