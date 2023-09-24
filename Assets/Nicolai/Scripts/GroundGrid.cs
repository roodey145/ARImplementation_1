using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrid : MonoBehaviour
{
    public int hight = 5;
    public int wide = 5;
    public GameObject gridGround;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -hight; i < hight; i++)
        {
            for (int j = -wide; j < wide; j++)
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
