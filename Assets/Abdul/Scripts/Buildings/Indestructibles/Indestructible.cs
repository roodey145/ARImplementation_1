using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Indestructible
{
    [SerializeField] internal int id = -1;
    [SerializeField] internal int level = 1;
    [SerializeField] internal BuildingType type;
    [SerializeField] internal int appliedX = -1;
    [SerializeField] internal int appliedZ = -1;
    internal long lastUpdated = -1;
    
    public Indestructible(int id, int level, BuildingType type, int x, int z)
    {
        this.id = id;
        this.level = level;
        this.type = type;
        appliedX = x;
        appliedZ = z;
        lastUpdated = DateTime.Now.Ticks;
    }

    internal void UpdateTime()
    {
        lastUpdated = DateTime.Now.Ticks;
    }

    internal double GetLastUpdatedTimeInSeconds()
    {
        if(lastUpdated < 0)
            lastUpdated = DateTime.Now.Ticks;

        return new TimeSpan(lastUpdated).TotalSeconds;
    }

    internal double GetTimeDifferenceInSeconds()
    {
        return new TimeSpan(DateTime.Now.Ticks - lastUpdated).TotalSeconds;
    }
}
