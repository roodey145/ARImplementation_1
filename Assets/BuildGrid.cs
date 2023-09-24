using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    public bool isUsed;
    private Material cubeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        cubeMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed)
        {
            cubeMaterial.color = Color.red;
        }
        else
        {
            cubeMaterial.color = Color.yellow;
        }
    }
}
