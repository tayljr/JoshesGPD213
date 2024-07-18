using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AStar : SerializedMonoBehaviour
{
    public Vector3Int startPos = new Vector3Int(0, 0, 0);
    public Vector3Int targetPos = new Vector3Int(2, 0, 0);
    public bool instantScan = false;
    public float loopSpeed = 0.000001f;
    public int loopNumber = 10;
    public bool showBlocked = true;
    public bool showOpen = true;
    public bool showClosed = true;
    public bool showNodes = true;
    public List<Node> open;
    public List<Node> closed;
    public WorldScanner worldScanner;
    private List<Node> wasOpen = new List<Node>();
    private List<Node> nextOpen = new List<Node>();
    private Node node;
    private bool foundTarget = false;
    private List<Node> finalPath = new List<Node>();
    
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
        StartCoroutine(StartFill(startPos));
    }
    
    IEnumerator StartFill(Vector3Int currentPos)
    {
        //todo handle diagonals through walls
        open.Clear();
        closed.Clear();
        foundTarget = false;
        //need to change current pos to get pos from node ... i think + inside while loop
        //done
        open.Add(worldScanner.gridOfObstacles[currentPos.x, currentPos.y, currentPos.z]);
        int currentLoop = 0;
        while (open.Count > 0)
        {
            //don't go in order, pick the lowest score
            // https://stackoverflow.com/questions/14967569/getting-the-lowest-value-of-a-liststruct-int Jim Mischel
            int best = 0;
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < open[best].fCost)
                {
                    best = i;
                }
            }

            Node bestNode = open[best];
                if (currentLoop >= loopNumber)
                {
                    if(!instantScan)
                    {
                        yield return new WaitForSeconds(loopSpeed);
                    }
                    currentLoop = 0;
                }
                currentLoop++;
                Fill(currentPos, bestNode);

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
            if (foundTarget)
            {
                break;
            }
        }

        Node currentNode = worldScanner.gridOfObstacles[targetPos.x, targetPos.y, targetPos.z];
        //todo find path
        while (currentNode.parent != null)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();
    }

    private void Fill(Vector3Int currentPos, Node node)
    {
        currentPos = node.pos;
        if(!closed.Contains(node))
        {
            closed.Add(node);
        }
        wasOpen.Add(node);

        if (currentPos == targetPos)
        {
            foundTarget = true;
            return;
        }
        for(int x = -1; x < 2; x++)
        {
            for (int z = -1; z < 2; z++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if(!foundTarget)
                    {
                        //check to see the node exists please :)
                        //done
                        Vector3Int nextPos = new Vector3Int(currentPos.x + x, currentPos.y + y, currentPos.z + z);
                        if (nextPos.x < 0 || nextPos.y < 0 || nextPos.z < 0 || nextPos.x >= worldScanner.size.x || nextPos.y >= worldScanner.sizeY || nextPos.z >= worldScanner.size.z)
                        {
                            continue;
                        }

                        Node nextWorldNode = worldScanner.gridOfObstacles[nextPos.x, nextPos.y, nextPos.z];
                        //if (closed.Contains(nextWorldNode) || open.Contains(nextWorldNode) || wasOpen.Contains(nextWorldNode) || nextOpen.Contains(nextWorldNode))
                        // if (open.Contains(nextWorldNode) || wasOpen.Contains(nextWorldNode) || nextOpen.Contains(nextWorldNode))
                        if (closed.Contains(nextWorldNode))
                        {
                            continue;
                        }

                        if (!nextWorldNode.isBlocked)
                        {
                            float newHCost = Vector3.Distance(nextPos, targetPos);
                            float newGCost = node.gCost + Vector3.Distance(nextPos, currentPos);
                            float newFCost = newHCost + newGCost;
                            //or if (closed.Contains(nextWorldNode) || open.Contains(nextWorldNode) || nextOpen.Contains(nextWorldNode))
                            // if (nextWorldNode.parent != null)
                            // {
                            if (nextWorldNode != worldScanner.gridOfObstacles[startPos.x, startPos.y, startPos.z])
                            {
                                if (newGCost < nextWorldNode.gCost || !open.Contains(nextWorldNode))
                                {
                                    nextWorldNode.fCost = newFCost;
                                    nextWorldNode.gCost = newGCost;
                                    nextWorldNode.hCost = newHCost;
                                    nextWorldNode.parent = node;
                                    if(!open.Contains(nextWorldNode) && !nextOpen.Contains(nextWorldNode))
                                    {
                                        nextOpen.Add(nextWorldNode);
                                    }
                                }
                                // else if (!open.Contains(nextWorldNode) && !closed.Contains(nextWorldNode))
                                // {
                                //     nextWorldNode.fCost = newFCost;
                                //     nextWorldNode.gCost = newGCost;
                                //     nextWorldNode.hCost = newHCost;
                                //     nextWorldNode.parent = node;
                                // }
                            }
                            // }
                            // else if (nextWorldNode != worldScanner.gridOfObstacles[startPos.x, startPos.y, startPos.z])
                            // {
                            //     nextWorldNode.fCost = newFCost;
                            //     nextWorldNode.gCost = newGCost;
                            //     nextWorldNode.hCost = newHCost;
                            //     nextWorldNode.parent = node;
                            // }
                        }
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if(isActiveAndEnabled)
        {
            if (worldScanner != null)
            {
                for (int x = 0; x < worldScanner.size.x; x++)
                {
                    for (int z = 0; z < worldScanner.size.z; z++)
                    {
                        for (int y = 0; y < worldScanner.sizeY; y++)
                        {
                            if (worldScanner.gridOfObstacles != null)
                            {
                                if (worldScanner.gridOfObstacles[x, y, z].isBlocked && showBlocked)
                                {
                                    Gizmos.color = Color.red;
                                    if (targetPos == new Vector3Int(x, y, z))
                                    {
                                        Gizmos.color = new Color(1f, 0.5f, 0f, 1f);
                                    }
                                    if (startPos == new Vector3Int(x, y, z))
                                    {
                                        Gizmos.color = new Color(0.5f, 0f, 1f, 1f);
                                    }
                                    //Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                                }
                                else
                                {
                                    Gizmos.color = Color.clear;
                                    //Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                                }

                                if (open.Contains(worldScanner.gridOfObstacles[x, y, z]) || nextOpen.Contains(worldScanner.gridOfObstacles[x, y, z]))
                                {
                                    Gizmos.color = new Color(1f, 0.9215686f, 0.01568628f, 0.5f);
                                    if (!showOpen)
                                    {
                                        Gizmos.color = Color.clear;
                                    }
                                }

                                if (closed.Contains(worldScanner.gridOfObstacles[x, y, z]))
                                {
                                    Gizmos.color = Color.green;
                                    if (!showClosed)
                                    {
                                        Gizmos.color = Color.clear;
                                    }
                                }
                                
                                if (showNodes)
                                {
                                    if (targetPos == new Vector3Int(x, y, z) && !worldScanner.gridOfObstacles[x, y, z].isBlocked)
                                    {
                                        Gizmos.color = Color.yellow;
                                    }
                                    if (startPos == new Vector3Int(x, y, z) && !worldScanner.gridOfObstacles[x, y, z].isBlocked)
                                    {
                                        Gizmos.color = Color.blue;
                                    }

                                    if (finalPath.Contains(worldScanner.gridOfObstacles[x, y, z]))
                                    {
                                        Gizmos.color = Color.magenta;
                                    }
                                    Gizmos.DrawCube(transform.position + new Vector3(x * worldScanner.nodeSize.x, y * worldScanner.nodeSize.y, z * worldScanner.nodeSize.z), worldScanner.nodeSize);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
