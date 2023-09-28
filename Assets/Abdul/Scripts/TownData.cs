using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownData : MonoBehaviour
{
    private int _width = 25;
    private int _height = 25;

    // Start is called before the first frame update
    void Start()
    {
        // Retrive town data
        _retriveTownData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Getter
    public int Width()
    { 
        return _width; 
    }

    public int Height()
    {
        return _height;
    }



    // Server side API calls
    private void _retriveTownData()
    {
        // Get Data (size, height, etc...)
    }
}
