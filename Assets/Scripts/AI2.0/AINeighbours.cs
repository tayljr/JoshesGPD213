using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AINeighbours : MonoBehaviour, IAIBehaviour
{
    public AIMovementManager movementManager;
    
    public float neighbourDistance = 5f;

    public List<GameObject> neighbours;
    
    void Start()
    {
        if (movementManager == null)
        {
            movementManager = GetComponentInParent<AIMovementManager>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        Collider[] neighbourColliders = Physics.OverlapSphere(transform.position, neighbourDistance, LayerMask.GetMask("AI"));
        neighbours.Clear();
        foreach (Collider neighbourCollider in neighbourColliders)
        {
            if (neighbourCollider.gameObject == gameObject)
            {
                continue;
            }
            // Debug.DrawLine(transform.position, neighbour.transform.position, Color.blue);
            Vector3 dir = neighbourCollider.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir.normalized, out hit, dir.magnitude, ~LayerMask.GetMask("AI")))
            {
                //Debug.Log(hit.transform.gameObject.layer);
                Debug.DrawRay(transform.position, dir, Color.magenta);
            }
            else
            {
                neighbours.Add(neighbourCollider.gameObject);
                Debug.DrawRay(transform.position, dir, Color.blue);
            }
        }
        
    }

    public int Priority => (int)behaviourEnum.neighbourCheck;
    public void Execute(ref int points)
    {
        movementManager.SetNeighbours(neighbours);
    }
}
