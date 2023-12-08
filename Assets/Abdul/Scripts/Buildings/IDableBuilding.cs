using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDableBuilding : InteractableBuilding
{
    internal int ID = -1;
    protected Indestructible _indestructibleInfo = null;

    // Start is called before the first frame update
    protected new void Awake()
    {
        base.Awake();

        //print("Awake ID: " + ID);
        // Get the ID from the building info container
        if (ID < 1)
        {
            //print("Called In Awake");
            IndestructiblesManager.AddNewBuilding(this); // Indestructible added to the manager and to this building
        }
        else
            _indestructibleInfo = IndestructiblesManager.GetIndestructible(ID);
    }

    internal virtual Indestructible CreateIndestructible(int  id)
    {
        //print("Indestructible: " + _indestructibleInfo);
        if (_indestructibleInfo == null)
        {
            ID = id;
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

    protected override void Remove()
    {
        base.Remove();
        ListItemsManager.instance.ReturnBuilding(_indestructibleInfo);
    }

    internal override void LevelUp()
    {
        base.LevelUp();

        _indestructibleInfo.level = _level;
    }

    private void OnDestroy()
    {
        // Make sure to unregister this building from the buildings battle progress
        BattaleProgress.UnregisterBuilding(this);
    }
}
