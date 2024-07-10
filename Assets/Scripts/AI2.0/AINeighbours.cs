using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AINeighbours : MonoBehaviour
{
    public float neighbourDistance = 5f;

    public List<GameObject> neighbours;

    public int numNeighbours = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //layermask being wierd
        Collider[] neighbourColliders = Physics.OverlapSphere(transform.position, neighbourDistance, 1<<6);
        neighbours.Clear();
        foreach (Collider neighbourCollider in neighbourColliders)
        {
            neighbours.Add(neighbourCollider.gameObject);
        }
        foreach (GameObject neighbour in neighbours)
        {
            Debug.DrawLine(transform.position, neighbour.transform.position, Color.blue);
        }
    }
}
