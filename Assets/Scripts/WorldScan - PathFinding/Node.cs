using System;
using UnityEngine;

[Serializable]
public class Node
{
    public bool isBlocked;
    public Vector3Int pos = new Vector3Int(0, 0, 0);

    public float gCost;
    public float hCost;
    public float fCost;

    public Node parent;
    

    public Node()
    {
        fCost = hCost + gCost;
    }
}
