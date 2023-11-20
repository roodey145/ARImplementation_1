using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundData 
{
    public static int width { get; private set; } = 10; // The actual width is width*2 + 1 (x-axis)
    public static int length { get; private set; } = 5; // The actual length is length*2 +1 (z-axis)
    public static GroundBlock[,] groundBlocks { get; private set; }

    public static void awake()
    {
        if(groundBlocks == null)
            groundBlocks = new GroundBlock[width * 2 + 1, length * 2 + 1]; // 2-Dimention will allow for faster retrive of the data
    }

    public static GroundBlock GetGroundBlock(int x, int z)
    {
        //MonoBehaviour.print($"Before: ({x}, {z}) \n ({groundBlocks.GetLength(0)}, {groundBlocks.GetLength(1)})");
        // the actual position is shifted by "width, length" since the incoming x,z can be negative.
        x += width; 
        z += length;
        MonoBehaviour.print($"After: ({x}, {z}) \n ({groundBlocks[x, z]})");
        return groundBlocks[x, z];
    }

    public static void AssignGroundBlock(int x, int z, GroundBlock groundBlock)
    {
        x += width;
        z += length;
        //MonoBehaviour.print($"({x}, {z}) {groundBlock}");
        groundBlocks[x, z] = groundBlock;
    }

    internal static int Width()
    {
        return width * 2 + 1;
    }

    internal static int Length()
    {
        return length * 2 + 1;
    }
}
