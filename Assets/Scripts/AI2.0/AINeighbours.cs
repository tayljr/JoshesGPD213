using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AINeighbours : MonoBehaviour
{
    public float neighbourDistance = 5f;

    public Collider[] neighbours;

    public int numNeighbours = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //layermask being wierd
        neighbours = Physics.OverlapSphere(transform.position, neighbourDistance, LayerMask.NameToLayer("AI"));
        foreach (Collider neighbour in neighbours)
        {
            Debug.DrawLine(transform.position, neighbour.transform.position, Color.blue);
        }
    }
}
