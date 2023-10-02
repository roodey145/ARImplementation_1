using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    public static int X = 0;
    public static int Z = 0;


    [SerializeField] private int _x = 0;
    [SerializeField] private int _z = 0;

    public static GameObject player = null;
    public static bool selected = false;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            select();
        }
    }

    // Setter
    public void SetPosition(int x, int z)
    {
        _x = x;
        _z = z;
    }

    public void hover()
    {
        X = GetX();
        Z = GetZ();


        //player.transform.position = new Vector3(
        //    transform.position.x,
        //    player.transform.position.y,
        //    transform.position.z);
    }

    public void select()
    {
        selected = true;
    }

    public void selectExit()
    {
        selected = false;
        player.transform.position = new Vector3(
            transform.position.x, 
            player.transform.position.y, 
            transform.position.z);
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
