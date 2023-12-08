using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class IndestructiblesManager
{
    private static List<Indestructible> _indestructibles = new List<Indestructible>();

    internal static void AddNewBuilding(IDableBuilding building)
    {
        //MonoBehaviour.print(building);
        _indestructibles.Add(building.CreateIndestructible(GetNewID()));
    }

    internal static int GetNewID()
    {// This should get the data from the server...
        int id = _indestructibles.Count + 1;
        //MonoBehaviour.print("Current ID: " + id);
        return id;
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
