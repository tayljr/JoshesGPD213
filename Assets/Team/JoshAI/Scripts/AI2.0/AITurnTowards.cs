using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurnTowards : MonoBehaviour, IAIBehaviour
{
    public AIMovementManager movementManager;

    public Transform target;
    private Vector3 targetDir;
    public Vector3 targetPos;
    private float angle;

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
        if (target)
        {
            targetPos = target.position;
        }

        targetDir = targetPos - transform.position;
        angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        if (angle > 1)
        {
            angle = 1;
        }

        if (angle < -1)
        {
            angle = -1;
        }
    }

    public void NewTarget(Vector3 newTarget)
    {
        target = null;
        targetPos = newTarget;
    }

    public behaviourEnum Priority => behaviourEnum.turnTowards;

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
        //Debug.Log("Turn " + pointsNeeded);

        if (points >= pointsNeeded)
        {
            movementManager.AddTurn(angle * movementManager.maxTurn);
            points -= pointsNeeded;
        }
        else
        {
            float multiplier = points / pointsNeeded;

            movementManager.AddTurn(angle * movementManager.maxTurn * multiplier);
            points = 0;
        }
    }

    private int CalculatePointsNeeded()
    {
        return Mathf.RoundToInt(Mathf.Abs(angle * 5));
    }
}