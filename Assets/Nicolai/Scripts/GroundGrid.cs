using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrid : MonoBehaviour
{
    public int length = 5;
    public int width = 5;
    public GameObject gridGround;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -length; i < length; i++)
        {
            for (int j = -width; j < width; j++)
            {

                var gridPos=Instantiate(gridGround, new Vector3(i, 0, j), Quaternion.identity);
                gridPos.transform.parent = gameObject.transform;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
