using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class DijkstraFill : SerializedMonoBehaviour
{
    public Vector3Int startPos = new Vector3Int(0, 0, 0);
    public List<Node> open;
    public List<Node> closed;
    public WorldScanner worldScanner;
    private List<Node> wasOpen = new List<Node>();
    private List<Node> nextOpen = new List<Node>();
    
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
        //todo need to change current pos to get pos from node ... i think + inside while loop
        open.Add(worldScanner.gridOfObstacles[currentPos.x, currentPos.y, currentPos.z]);
        while (open.Count > 0)
        {
            foreach (Node node in open)
            {
                currentPos = node.pos;
                if(!closed.Contains(node))
                {
                    closed.Add(node);
                }
                wasOpen.Add(node);

                for(int x = -1; x < 2; x++)
                {
                    for (int z = -1; z < 2; z++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            //check to see the node exists please :)
                            Vector3Int nextPos = new Vector3Int(currentPos.x + x , currentPos.y + y, currentPos.z + z);
                            if(nextPos.x < 0 || nextPos.y < 0 || nextPos.z < 0 || nextPos.x >= worldScanner.size.x || nextPos.y >= worldScanner.sizeY || nextPos.z >= worldScanner.size.z)
                            {
                                continue;
                            }
                            Node nextWorldNode = worldScanner.gridOfObstacles[nextPos.x, nextPos.y, nextPos.z];
                            if (closed.Contains(nextWorldNode) || open.Contains(nextWorldNode))
                            {
                                continue;
                            }
                            if (!nextWorldNode.isBlocked)
                            {
                                nextOpen.Add(nextWorldNode);
                            }
                        }
                    }
                }
            }

            foreach (Node node in wasOpen)
            {
                open.Remove(node);
            }
            wasOpen.Clear();
            foreach (Node node in nextOpen)
            {
                open.Add(node);
            }
            nextOpen.Clear();
        }
    }
}
