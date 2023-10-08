using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAdder : MonoBehaviour
{
    private static string _demosPath = "Town/Demos/";
    [SerializeField] private string _buildingName;


    public void AddDemo()
    {
        // Get the demo
        GameObject demo = Resources.Load<GameObject>(_demosPath + _buildingName);
        if(demo == null)
        {
            throw new System.Exception("The demo with the name (" + 
                _buildingName + ") deos not exists inside the folder (" + _demosPath + " )!");
        }
        Instantiate(demo);
        Destroy(gameObject, 0f);
    }
}
