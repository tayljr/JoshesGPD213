using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWander : MonoBehaviour, IAIBehaviour
{
    public AIMovementManager movementManager;
    private float angle;
    
    private int randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        if (movementManager == null)
        {
            movementManager = GetComponentInParent<AIMovementManager>();
        }
        
        randomOffset = Random.Range(-200, 200);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angle = Mathf.PerlinNoise1D(randomOffset + Time.time) * 2 - 1;
    }

    public int Priority => 1;
    
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
        Debug.Log("Wander " + pointsNeeded);

        if (points >= pointsNeeded)
        {
            movementManager.AddTurn(angle * movementManager.maxTurn);
            
            points -= pointsNeeded;
        }
    }
    
    private int CalculatePointsNeeded()
    {
        return Mathf.RoundToInt(Mathf.Abs(angle) * 5);
    }
}
