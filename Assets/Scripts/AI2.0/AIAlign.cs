using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAlign : MonoBehaviour, IAIBehaviour
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
        
        
        //Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector
        //todo attempt to implement vector3.cross into the advanced void
        cross = Vector3.Cross(transform.forward, targetDir);
    }

    public Vector3 CalculateDir(List<GameObject> neighbours)
    {
        if (neighbours.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 alignDir = Vector3.zero;

        foreach (GameObject neighbour in neighbours)
        {
            alignDir += neighbour.transform.forward;
        }

        alignDir /= neighbours.Count;

        //Debug.Log(alignMove);
        return alignDir;
    }
    
    public int Priority => (int)behaviourEnum.alignment;
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
