using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indestructible
{
    internal int id = -1;
    internal int level = 1;
    internal BuildingType type;
    internal int appliedX = -1;
    internal int appliedZ = -1;
    internal long lastUpdated = 0;
    
    public Indestructible(int id, int level, BuildingType type, int x, int z)
    {
        this.id = id;
        this.level = level;
        this.type = type;
        appliedX = x;
        appliedZ = z;
        lastUpdated = System.DateTime.Now.Ticks;
    }

    internal void UpdateTime()
    {
        lastUpdated = System.DateTime.Now.Ticks;
    }

    internal double GetLastUpdatedTimeInSeconds()
    {
        return new System.TimeSpan(lastUpdated).TotalSeconds;
    }

    internal double GetTimeDifference()
    {
        return new System.TimeSpan(System.DateTime.Now.Ticks - lastUpdated).TotalSeconds;
    }
}
