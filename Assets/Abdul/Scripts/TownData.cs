using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TownData : MonoBehaviour
{
    [SerializeField] private Indestructible[] buildings;
    [SerializeField] private ListItemsManager _listItemsManager;
    private int _width = 25; // The x-axis
    private int _length = 25; // The z-axis

    private void Awake()
    {
        //GroundData.awake();
        for(int i = 0; i < buildings.Length; i++)
        {
            _listItemsManager.ReturnBuilding(buildings[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //_width = GetComponent<BuildGroundGrid>().Width();
        //_length = GetComponent<BuildGroundGrid>().Length();

        _width = GroundData.Width();
        _length = GroundData.Length();
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
