using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    private int _x = 0;
    private int _z = 0;

    // Setter
    public void SetPosition(int x, int z)
    {
        _x = x;
        _z = z;
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
