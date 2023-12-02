using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ManagerListItemData
{
    public GameObject listItemModel;
    public BuildingType buildingType;
    public string[] UIItemListPath; // The path should be organized level-wise
}
