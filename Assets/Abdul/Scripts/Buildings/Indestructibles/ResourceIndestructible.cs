using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIndestructible : Indestructible
{
    protected float resources = 0;
    public ResourceIndestructible(int id, int level, BuildingType type, int x, int z, float resources) : base(id, level, type, x, z)
    {
        this.resources = resources;
    }

    internal void UpdateResources(float resources)
    {
        this.resources = resources;
        UpdateTime(); // Indicate that the info has been updated;
    }
    internal float GetResources()
    {
        return resources;
    }
}
