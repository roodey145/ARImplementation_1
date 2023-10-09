using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    public static int X = 0;
    public static int Z = 0;


    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;

    public static GameObject player = null;
    public static bool selected = false;
    public static GameObject demo = null;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Select();
        }
    }

    // Setter
    public void SetPosition(int x, int z)
    {
        _x = x;
        _z = z;
    }

    public void Hover()
    {
        X = GetX();
        Z = GetZ();


        //player.transform.position = new Vector3(
        //    transform.position.x,
        //    player.transform.position.y,
        //    transform.position.z);
    }

    public void Select()
    {

        selected = true; 

    }

    public void SelectExit()
    {
        selected = false;
        if (demo == null)
        { // Teleportation
            player.transform.position = new Vector3(
                transform.position.x,
                player.transform.position.y,
                transform.position.z);
        }
        else
        { // Placing of the demo
            demo.GetComponent<BuildingData>().PlaceModel(_x, _z);
            demo = null;
        }
    }

    public TextMeshProUGUI text;
    public void Activate()
    {
        text.text = "Activated";
    }

    public void Deactivate()
    {
        text.text = "Deactivated";
    }

    public void Focus()
    {
        text.text = "Focused";
    }

    public void Blur()
    {
        text.text = "Blur";
    }

    // Getter
    public int GetX()
    {
        return _x;
    }

    public int GetZ()
    {
        return _z;
    }
}
