using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DijkstraFill : SerializedMonoBehaviour
{
    public Vector3Int startPos = new Vector3Int(0, 0, 0);
    public List<Node> open;
    public List<Node> closed;
    public WorldScanner worldScanner;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartFill()
    {
        StartFill(startPos);
    }
    public void StartFill(Vector3Int currentPos)
    {
        open.Add(worldScanner.gridOfObstacles[currentPos.x, currentPos.y, currentPos.z]);
        while (open.Count > 0)
        {
            foreach (Node node in open)
            {
                if(!closed.Contains(node))
                {
                    closed.Add(node);
                }
                open.Remove(node);

                for(int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        for (int z = -1; z < 2; z++)
                        {
                            //check to see the node exists please :)
                            Vector3Int nextPos = new Vector3Int(currentPos.x + x , currentPos.y + y, currentPos.z + z);
                            Node nextWorldNode = worldScanner.gridOfObstacles[nextPos.x, nextPos.y, nextPos.z];
                            if (closed.Contains(nextWorldNode) || open.Contains(nextWorldNode))
                            {
                                continue;
                            }
                            else if (!nextWorldNode.isBlocked)
                            {
                                open.Add(nextWorldNode);
                            }
                        }
                    }
                }
            }
        }
    }
}
