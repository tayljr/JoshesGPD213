using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DijkstraFill : SerializedMonoBehaviour
{
    public Vector3Int startPos = new Vector3Int(0, 0, 0);
    [FormerlySerializedAs("showScan")] public bool randomFlow = false;
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
        open.Clear();
        closed.Clear();
        //todo need to change current pos to get pos from node ... i think + inside while loop
        //done
        open.Add(worldScanner.gridOfObstacles[currentPos.x, currentPos.y, currentPos.z]);
        int currentLoop = 0;
        while (open.Count > 0)
        {
            if(randomFlow)
            {
                if (currentLoop >= loopNumber)
                {
                    if(!instantScan)
                    {
                        yield return new WaitForSeconds(loopSpeed);
                    }
                    currentLoop = 0;
                }
                currentLoop++;
                node = open[Random.Range(0, open.Count)];
                {
                    Fill(currentPos, node);
                }
            }
            else
            {
                foreach (Node node in open)
                {
                    if (currentLoop >= loopNumber)
                    {
                        if(!instantScan)
                        {
                            yield return new WaitForSeconds(loopSpeed);
                        }
                        currentLoop = 0;
                    }
                    currentLoop++;
                    Fill(currentPos, node);
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

    private void Fill(Vector3Int currentPos, Node node)
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
                    //done
                    Vector3Int nextPos = new Vector3Int(currentPos.x + x , currentPos.y + y, currentPos.z + z);
                    if(nextPos.x < 0 || nextPos.y < 0 || nextPos.z < 0 || nextPos.x >= worldScanner.size.x || nextPos.y >= worldScanner.sizeY || nextPos.z >= worldScanner.size.z)
                    {
                        continue;
                    }
                    Node nextWorldNode = worldScanner.gridOfObstacles[nextPos.x, nextPos.y, nextPos.z];
                    if (closed.Contains(nextWorldNode) || open.Contains(nextWorldNode) || wasOpen.Contains(nextWorldNode) || nextOpen.Contains(nextWorldNode))
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
                                    if (startPos == new Vector3Int(x, y, z) && !worldScanner.gridOfObstacles[x, y, z].isBlocked)
                                    {
                                        Gizmos.color = Color.blue;
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
