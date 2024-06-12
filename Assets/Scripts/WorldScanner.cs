using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScanner : MonoBehaviour
{
    public Vector3Int size;
    public Node[,] gridOfObstacles;
    public LayerMask layerMask;
    public bool debug = false;
    
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
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                if (gridOfObstacles != null && gridOfObstacles[x, z].isBlocked)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawCube(new Vector3(x, 0, z), Vector3.one);
            }
        }
    }

    private void ScanWorld()
    {
        gridOfObstacles = new Node[size.x, size.z];
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                gridOfObstacles[x, z] = new Node();
                if (Physics.CheckBox(new Vector3(x, 0, z), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, layerMask))
                {
                    // Something is there
                    gridOfObstacles[x, z].isBlocked = true;
                }
            }
        }
    }
}
