using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISeperation : MonoBehaviour, IAIBehaviour
{
    public AIMovementManager movementManager;
    
    public Vector3 cross;
    // Start is called before the first frame update
    void Start()
    {
        if (movementManager == null)
        {
            movementManager = GetComponentInParent<AIMovementManager>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetDir = CalculateDir(movementManager.myNeighbours);
        
        //see AIAlign.cs for more info
        cross = Vector3.Cross(transform.forward, targetDir);
    }

    public Vector3 CalculateDir(List<GameObject> neighbours)
    {
        if (neighbours.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 SeperateDir = Vector3.zero;
        foreach (GameObject neighbour in neighbours)
        {
            SeperateDir += (transform.position - neighbour.transform.position).normalized;
        }

        SeperateDir /= neighbours.Count;

        return SeperateDir;

    }
    
    public behaviourEnum Priority => behaviourEnum.seperation;
    public void Execute(ref int points)
    {
        if (!isActiveAndEnabled)
        {
            return;
        }
        
        if (points <= 0)
        {
            return;
        }

        int pointsNeeded = CalculatePointsNeeded();
        //Debug.Log("Align " + pointsNeeded);

        if (points >= pointsNeeded)
        {
            movementManager.AddTurn(cross.y * movementManager.maxTurn);
            points -= pointsNeeded;
        }
        else
        {
            float multiplier = points / pointsNeeded;

            movementManager.AddTurn(cross.y * movementManager.maxTurn * multiplier);
            points = 0;
        }
    }

    private int CalculatePointsNeeded()
    {
        return Mathf.RoundToInt(Mathf.Abs(cross.y * 2));
    }
}
