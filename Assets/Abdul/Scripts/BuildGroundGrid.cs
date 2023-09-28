using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGroundGrid : MonoBehaviour
{
    [SerializeField] private int _height = 5;
    [SerializeField] private int _width = 5;
    [SerializeField] private string _groundBlocksPath = "Ground/";
    [SerializeField] private GameObject[] _groundBlocks;
    // Start is called before the first frame update
    void Start()
    {
        _groundBlocks = GetGroundBlockPrefabs();
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
