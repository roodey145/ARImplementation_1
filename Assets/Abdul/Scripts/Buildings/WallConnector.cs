using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConnector : MonoBehaviour
{
    [SerializeField] private GameObject[] _children; // The walls in the four directions
    private BuildingData _buildingData;
    private DemoData _demoData;
    private int _x;
    private int _z;

    private bool[] _connectedDirections = new bool[4];

    // Start is called before the first frame update
    void Awake()
    {
        // Get the children (Left, Top, Right, Bottom)
        _children = new GameObject[transform.childCount];
        for(int i = 0; i < _children.Length; i++)
        {
            _children[i] = transform.GetChild(i).gameObject;
            _children[i].SetActive(false);
        }

        // Get the building data
        _buildingData = GetComponent<BuildingData>();

        if(_buildingData != null )
            _buildingData.RegisterLocationUpdateCallback(_LocationUpdateCallback);

        _demoData = GetComponent<DemoData>();

        if(_demoData != null)
            _demoData.RegisterLocationUpdateCallback(_LocationUpdateCallback);
        //print("Demo: " +  _buildingData.isDemo());
        //_UpdateToSurroundingWalls(connect: true, _buildingData.isDemo());
    }

    private void _LocationUpdateCallback(int x, int z)
    {
        // Disconnect first from the old position
        DisconnectDemo();
        ResetToActualWallConnection();
        // Update the x, y to the new position and then connect the walls
        _x = x;
        _z = z;
        _UpdateToSurroundingWalls(connect: true, _demoData != null);
    }

    private void _UpdateToSurroundingWalls(bool connect, bool demo)
    {
        // Get the block at the left direction
        BuildingData buildingData;
        for(int i = 0; i < _children.Length; i++)
        {
            buildingData = GetBlockBuildingData((DirectionEnum)i);
            if (buildingData != null)
            {
                // Check if the buildiing is a wall
                if (buildingData.BuildingType == BuildingType.Wall)
                {
                    if(connect)
                    {
                        // Connect this wall 
                        ConnectWallTo((DirectionEnum)i, demo); 
                        // Connect the other wall
                        buildingData.gameObject.GetComponent<WallConnector>().ConnectWallTo(DirectionEnumUtility.GetOpposite((DirectionEnum)i), demo);
                    }
                    else
                    {
                        // Connect this wall 
                        DisconnectWall((DirectionEnum)i, demo);
                        // Connect the other wall
                        if(!demo)
                            buildingData.gameObject.GetComponent<WallConnector>().DisconnectWall(DirectionEnumUtility.GetOpposite((DirectionEnum)i), demo);
                        else
                            buildingData.gameObject.GetComponent<WallConnector>().ResetToActualWallConnection();
                    }
                    
                }
            }
        }
    }


    private BuildingData GetBlockBuildingData(DirectionEnum direction)
    {
        BuildingData groundBlockBuildingData = null;

        switch(direction)
        {
            case DirectionEnum.Left:
                groundBlockBuildingData = GetLeftBlockBuildingData();
                break;
            case DirectionEnum.Top:
                groundBlockBuildingData = GetTopBlockBuildingData();
                break;
            case DirectionEnum.Right:
                groundBlockBuildingData = GetRightBlockBuildingData();
                break;
            case DirectionEnum.Bottom:
                groundBlockBuildingData = GetBottomBlockBuildingData();
                break;
        }

        return groundBlockBuildingData;
    }

    private BuildingData GetLeftBlockBuildingData()
    {
        int x = _x - 1;
        int z = _z;

        BuildingData groundBlockBuildingData = null;

        if ( x >= -GroundData.width )
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }
    private BuildingData GetTopBlockBuildingData()
    {
        int x = _x;
        int z = _z + 1;

        BuildingData groundBlockBuildingData = null;

        if (z <= GroundData.length)
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }
    private BuildingData GetRightBlockBuildingData()
    {
        int x = _x + 1;
        int z = _z;

        BuildingData groundBlockBuildingData = null;

        if (x <= GroundData.width)
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }
    private BuildingData GetBottomBlockBuildingData()
    {
        int x = _x;
        int z = _z - 1;

        BuildingData groundBlockBuildingData = null;

        if (z >= -GroundData.length)
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }


    public void ConnectWallTo(DirectionEnum direction, bool demo)
    {
        if(!demo)
        {
            _connectedDirections[(int)direction] = true;
        }
        _children[(int)direction].gameObject.SetActive(true);
    }

    public void DisconnectWall(DirectionEnum direction, bool demoRemoved)
    {
        if(!demoRemoved)
        {
            _connectedDirections[(int)direction] = false;
        }
        _children[(int)direction].gameObject.SetActive(false);
    }

    public void DisconnectDemo()
    {
        // Get the block at the left direction
        BuildingData buildingData;
        for (int i = 0; i < _children.Length; i++)
        {
            buildingData = GetBlockBuildingData((DirectionEnum)i);
            // Check if the buildiing is a wall
            if (buildingData != null && buildingData.BuildingType == BuildingType.Wall)
            {
                buildingData.gameObject.GetComponent<WallConnector>().ResetToActualWallConnection();
            }
        }
    }

    public void ResetToActualWallConnection()
    {
        for (int i = 0; i < _children.Length; i++)
        {
            if (_connectedDirections[i])
            {
                ConnectWallTo((DirectionEnum)i, true);
            }
            else
            {
                DisconnectWall((DirectionEnum)i, true);
            }
        }
    }


    private void OnDestroy()
    {
        // Make sure to notify the walls around so they will update and hide the connection to this wall
        if(_demoData != null)
        {
            DisconnectDemo();
        }
        else if(_buildingData != null) 
        {
            _UpdateToSurroundingWalls(connect: false, false);
        }
    }
}
