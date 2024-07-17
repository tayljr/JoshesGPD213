using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScanner : MonoBehaviour
{
    public Vector3Int size;
    public Vector3 nodeSize = new Vector3(1, 1, 1);
    public Node[,,] gridOfObstacles;
    public LayerMask layerMask;
    public bool scan2D = false;
    public int sizeY = 1;
    public bool debug = false;
    public bool showUnblocked = false;
    public bool showBlocked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ScanWorld();
    }

    // Update is called once per frame
    void Update()
    {
        if (debug)
        {
            ScanWorld();
        }
    }
    private void OnDrawGizmos()
    {
        if (scan2D)
        {
            sizeY = 1;
        }
        else
        {
            sizeY = size.y;
        }
        // for (int x = 0; x < size.x; x++)
        // {
        //     for (int z = 0; z < size.z; z++)
        //     {
        //         for (int y = 0; y < sizeY; y++)
        //         {
        //             if(gridOfObstacles != null)
        //             {
        //                 if (gridOfObstacles[x, y, z].isBlocked && showBlocked)
        //                 {
        //                     Gizmos.color = Color.red;
        //                     Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
        //                 }
        //                 else if (showUnblocked)
        //                 {
        //                     Gizmos.color = Color.green;
        //                     Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
        //                 }
        //             }
        //         }
        //     }
        // }
    }

    public void ScanWorld()
    {
        if (scan2D)
        {
            sizeY = 1;
        }
        else
        {
            sizeY = size.y;
        }
        gridOfObstacles = new Node[size.x, sizeY, size.z];
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    gridOfObstacles[x, y, z] = new Node();
                    gridOfObstacles[x, y, z].pos = new Vector3Int(x, y, z);
                    if (Physics.CheckBox(transform.position + new Vector3(x * nodeSize.x, y * nodeSize.y, z * nodeSize.z), new Vector3(0.5f * nodeSize.x, 0.5f * nodeSize.y, 0.5f * nodeSize.z), Quaternion.identity, layerMask))
                    {
                        // Something is there
                        gridOfObstacles[x, y, z].isBlocked = true;
                    }
                }
            }
        }
    }
}
