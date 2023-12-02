using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDableBuilding : InteractableBuilding
{
    internal int ID = -1;
    protected Indestructible _indestructibleInfo;

    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();

        // Get the ID from the building info container
        if (ID < 1)
            IndestructiblesManager.AddNewBuilding(this); // Indestructible added to the manager and to this building
        else
            _indestructibleInfo = IndestructiblesManager.GetIndestructible(ID);
    }

    internal virtual Indestructible CreateIndestructible(int  id)
    {
        if (_indestructibleInfo == null)
        {
            _indestructibleInfo = new Indestructible(id, _level, _buildingType, appliedX, appliedZ);
        }

        return _indestructibleInfo;
    }
    
    internal virtual void AssignIndestructible(Indestructible indestructible)
    {
        _indestructibleInfo = indestructible;

        // Update the data
        _level = indestructible.level;
        appliedX = indestructible.appliedX;
        appliedZ = indestructible.appliedZ;
    }

    internal int GetID()
    {
        return _indestructibleInfo != null ? _indestructibleInfo.id : -1;
    }

    internal void DataUpdated()
    {
        // Make sure to indicate that the data has been updated
        _indestructibleInfo.UpdateTime();
    }

    internal double GetLastUpdateTimeInSeconds()
    {
        return _indestructibleInfo.GetLastUpdatedTimeInSeconds();
    }
}
