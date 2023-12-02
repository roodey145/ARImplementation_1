using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class IndestructiblesManager
{
    private static List<Indestructible> _indestructibles = new List<Indestructible>();

    internal static void AddNewBuilding(IDableBuilding building)
    {
        _indestructibles.Add(building.CreateIndestructible(GetNewID()));
    }

    internal static int GetNewID()
    {// This should get the data from the server...
        return _indestructibles.Count + 1;
    }

    internal static Indestructible GetIndestructible(int id)
    {
        Indestructible indestructible = null;
        for(int i = 0; i < _indestructibles.Count; i++)
        {
            if (_indestructibles[i].id == id)
            {
                indestructible = _indestructibles[i];
                break;
            }
        }

        return indestructible;
    }

}
