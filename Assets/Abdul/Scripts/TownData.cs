using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TownData : MonoBehaviour
{
    private int _width = 25; // The x-axis
    private int _length = 25; // The z-axis

    private void Awake()
    {
        GroundData.awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        //_width = GetComponent<BuildGroundGrid>().Width();
        //_length = GetComponent<BuildGroundGrid>().Length();
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

    public int Length()
    {
        return _length;
    }



    // Server side API calls
    private void _retriveTownData()
    {
        // Get Data (size, height, etc...)
    }
}
