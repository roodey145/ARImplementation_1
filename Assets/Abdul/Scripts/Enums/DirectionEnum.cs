using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionEnum : int
{
    Left = 0,
    Top = 1,
    Right = 2,
    Bottom = 3,
}

public class DirectionEnumUtility
{
    public static DirectionEnum GetOpposite(DirectionEnum direction)
    {
        DirectionEnum oppositeDirection = direction;
        switch (direction)
        {
            case DirectionEnum.Left:
                oppositeDirection = DirectionEnum.Right; 
                break;
            case DirectionEnum.Top:
                oppositeDirection = DirectionEnum.Bottom;
                break;
            case DirectionEnum.Right:
                oppositeDirection = DirectionEnum.Left;
                break;
            case DirectionEnum.Bottom:
                oppositeDirection = DirectionEnum.Top;
                break;
        }
        return oppositeDirection;
    }
}