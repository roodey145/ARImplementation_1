using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConnector : MonoBehaviour
{
    [SerializeField] private GameObject[] _children; // The walls in the four directions
    private BuildingData _buildingData;

    // Start is called before the first frame update
    void Start()
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

        _UpdateToSurroundingWalls(connect: true);
    }

    private void _UpdateToSurroundingWalls(bool connect)
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
                        ConnectWallTo((DirectionEnum)i); 
                        // Connect the other wall
                        buildingData.gameObject.GetComponent<WallConnector>().ConnectWallTo(DirectionEnumUtility.GetOpposite((DirectionEnum)i));
                    }
                    else
                    {
                        // Connect this wall 
                        DisconnectWall((DirectionEnum)i);
                        // Connect the other wall
                        buildingData.gameObject.GetComponent<WallConnector>().DisconnectWall(DirectionEnumUtility.GetOpposite((DirectionEnum)i));
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
        int x = _buildingData.X - 1;
        int z = _buildingData.Z;

        BuildingData groundBlockBuildingData = null;

        if ( x >= -GroundData.width )
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }
    private BuildingData GetTopBlockBuildingData()
    {
        int x = _buildingData.X;
        int z = _buildingData.Z + 1;

        BuildingData groundBlockBuildingData = null;

        if (z <= GroundData.length)
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }
    private BuildingData GetRightBlockBuildingData()
    {
        int x = _buildingData.X + 1;
        int z = _buildingData.Z;

        BuildingData groundBlockBuildingData = null;

        if (x <= GroundData.width)
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }
    private BuildingData GetBottomBlockBuildingData()
    {
        int x = _buildingData.X;
        int z = _buildingData.Z - 1;

        BuildingData groundBlockBuildingData = null;

        if (z >= -GroundData.length)
        {
            groundBlockBuildingData = GroundData.GetGroundBlock(x, z).GetBuildingData();
        }

        return groundBlockBuildingData;
    }


    public void ConnectWallTo(DirectionEnum direction)
    {
        _children[(int)direction].gameObject.SetActive(true);
    }

    public void DisconnectWall(DirectionEnum direction)
    {
        _children[(int)direction].gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // Make sure to notify the walls around so they will update and hide the connection to this wall
        _UpdateToSurroundingWalls(connect: false);
    }
}
