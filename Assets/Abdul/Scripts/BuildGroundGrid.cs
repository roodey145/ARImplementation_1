using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildGroundGrid : MonoBehaviour
{
    [SerializeField] private int _height = 6;
    [SerializeField] private int _width = 5;
    [SerializeField] private string _groundBlocksPath = "Ground/";
    [SerializeField] private GameObject[] _groundBlocks;
    [SerializeField] private XRInteractionManager _interactionManager;
    // Start is called before the first frame update
    void Start()
    {
        _groundBlocks = GetGroundBlockPrefabs();
        //_interactionManager = FindObjectOfType<XRInteractionManager>();

        if(_groundBlocks.Length < 1)
        {
            throw new System.Exception("The blocks was not found");
        }

        int counter = 0;
        bool inverse = false;
        for (int z = -_height; z < _height; z++)
        {
            for (int x = -_width; x < _width; x++)
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
            }

            // To avoid that each column has the same color, we need to shift 
            // the block each row.
            inverse = !inverse;
        }
    }

    public GameObject[] GetGroundBlockPrefabs()
    {
        return Resources.LoadAll<GameObject>(_groundBlocksPath);
    }


    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
