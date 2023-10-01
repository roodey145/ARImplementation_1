using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        _groundBlocks = GetGroundBlockPrefabs();
        //_interactionManager = FindObjectOfType<XRInteractionManager>();
        _teleportationArea = GetComponent<TeleportationArea>();


        if (_groundBlocks.Length < 1)
        {
            throw new System.Exception("The blocks was not found");
        }

        int counter = 0;
        bool inverse = false;
        for (int z = -_length; z <= _length; z++)
        {
            for (int x = -_width; x <= _width; x++)
            {
                counter = counter < _groundBlocks.Length ? counter : 0;
                GameObject gridBlock = Instantiate(
                        _groundBlocks[ inverse ? (_groundBlocks.Length - 1) - (counter++) : (counter++)],
                        new Vector3(x, 0, z),
                        Quaternion.identity
                );
                gridBlock.transform.parent = gameObject.transform;

                gridBlock.GetComponent<GroundBlock>().SetPosition(x, z);
                gridBlock.GetComponent<XRSimpleInteractable>().interactionManager = _interactionManager;
                _teleportationArea.colliders.Add(gridBlock.GetComponent<Collider>());
            }

            // To avoid that each column has the same color, we need to shift 
            // the block each row.
            inverse = !inverse;
            counter -= 1;
        }
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
