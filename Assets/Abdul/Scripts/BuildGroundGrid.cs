using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildGroundGrid : MonoBehaviour
{
    [SerializeField] private int _width = 5; // The x-axis
    [SerializeField] private int _length = 6; // The z-axis
    [SerializeField] private string _groundBlocksPath = "Ground/";
    [SerializeField] private GameObject[] _groundBlocks;
    [SerializeField] private XRInteractionManager _interactionManager;
    [SerializeField] private TeleportationArea _teleportationArea;
    [SerializeField] private TeleportationProvider _teleportationProvider;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    // Start is called before the first frame update
    void Awake()
    {
        GroundData.awake();
        _groundBlocks = GetGroundBlockPrefabs();
        //_interactionManager = FindObjectOfType<XRInteractionManager>();
        //_teleportationArea = GetComponent<TeleportationArea>();
        //_teleportationArea = gameObject.AddComponent<TeleportationArea>();
        //_teleportationArea.interactionManager = _interactionManager;
        //_teleportationArea.teleportationProvider = _teleportationProvider;
        //_teleportationArea.matchDirectionalInput = true;
        //_teleportationArea.interactionLayers = InteractionLayerMask.GetMask("Teleport");

        // Get the width and length of the ground
        _width = GroundData.width;
        _length = GroundData.length;

        if (_groundBlocks.Length < 1)
        {
            throw new System.Exception("The blocks was not found");
        }

        int counter = 0;
        bool inverse = false;
        int index = 0;
        for (int z = -_length; z <= _length; z++)
        {
            for (int x = -_width; x <= _width; x++)
            {
                counter = counter < _groundBlocks.Length ? counter : 0;
                index = inverse ? (_groundBlocks.Length - 1) - (counter++) : (counter++);
                GameObject gridBlock = Instantiate(
                        _groundBlocks[index],
                        new Vector3(x, 0, z),
                        _groundBlocks[index].transform.rotation
                );
                gridBlock.transform.parent = gameObject.transform;

                // Get the ground block component
                GroundBlock groundBlock = gridBlock.GetComponent<GroundBlock>();
                groundBlock.SetPosition(x, z); // Assign the position
                GroundData.AssignGroundBlock(x, z, groundBlock); // Assign the ground block to the data.

                gridBlock.GetComponent<XRSimpleInteractable>().interactionManager = _interactionManager;
                //_teleportationArea.colliders.Add(gridBlock.GetComponent<Collider>());
            }

            // To avoid that each column has the same color, we need to shift 
            // the block each row.
            inverse = !inverse;
            counter -= 1;
        }


        //_teleportationArea = gameObject.AddComponent<TeleportationArea>();
        //_teleportationArea.interactionManager = _interactionManager;
        //_teleportationArea.teleportationProvider = _teleportationProvider;
        //_teleportationArea.matchDirectionalInput = true;
        //_teleportationArea.interactionLayers = InteractionLayerMask.GetMask("Teleport");
    }

    private void Start()
    {
        //_teleportationArea = gameObject.AddComponent<TeleportationArea>();
        //_teleportationArea.interactionManager = _interactionManager;
        //_teleportationArea.teleportationProvider = _teleportationProvider;
        //_teleportationArea.matchDirectionalInput = true;
        //_teleportationArea.interactionLayers = InteractionLayerMask.GetMask("Teleport");

        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
    }

    public GameObject[] GetGroundBlockPrefabs()
    {
        return Resources.LoadAll<GameObject>(_groundBlocksPath);
    }


    // Getter
    public int Width()
    {
        return _width;
    }

    public int Length()
    {
        return _length;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
