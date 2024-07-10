using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAvoid : MonoBehaviour, IAIBehaviour
{
    public AIMovementManager movementManager;
    
    public float maxAngle = 100;
    public int rays = 4;
    public float maxDistance = 5f;
    public float emergancyDistance = 5f;
    public float emergencyTurn = 1;
    public float maxSlow = 50f;
    
    private float angle = 0f;
    private float lastAngle = 0f;

    private float fowardDistance = 0;
    private float averageDisance = 0;
    private float totalStrength = 0;
    private int totalHit = 0;


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
        float totalDistance = 0f;
        float currentAngle = -maxAngle / 2f;
        //float averageDistance = 0f;
        Vector3 reflectionAverage = Vector3.zero;
        int hitRays = 0;
        for (int i = 0; i < rays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
            RaycastHit hit;
            Color hitColor = Color.red;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                hitRays++;
                totalDistance += hit.distance;
                if (currentAngle > 0)
                {
                    angle -= (maxDistance - hit.distance) / maxDistance;
                }

                if (currentAngle < 0)
                {
                    angle += (maxDistance - hit.distance) / maxDistance;
                }
                hitColor = Color.green;
                //angle *= (maxDistance - hit.distance) / maxDistance;
            }
            
            Debug.DrawRay(transform.position, direction * maxDistance, hitColor);
    
            float spreadAngle = maxAngle / (rays - 1);
            currentAngle += spreadAngle;
        }
        
        // emergency ray
         RaycastHit hitForward;
         Color forwardColor = Color.red;
         if (Physics.Raycast(transform.position, transform.forward, out hitForward, maxDistance/2))
         {
             if (hitForward.distance <= emergancyDistance)
             {
                 angle = emergencyTurn;
             }
        
             fowardDistance = hitForward.distance;
             forwardColor = Color.green;
         }
         Debug.DrawRay(transform.position, transform.forward * maxDistance/2, forwardColor);
        
        // Debug.Log(angle);

        if(hitRays > 0 )
        {
            averageDisance = ((maxDistance - totalDistance / hitRays) / maxDistance);
        }
        else
        {
            averageDisance = 0;
            if (fowardDistance > 0)
            {
                averageDisance = (maxDistance/2 - fowardDistance) / maxDistance/2;
            }
        }
        
        lastAngle = angle;
        totalHit = hitRays;
        angle = 0;
    }

    public int Priority => 3;

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
        Debug.Log("avoid " + pointsNeeded);
        
        
        if (pointsNeeded > 0)
        {
            if (points >= pointsNeeded)
            {
                //Debug.Log(lastAngle);
                movementManager.AddTurn(lastAngle * movementManager.maxTurn);
                movementManager.AddSpeed(-maxSlow * averageDisance);
                points = 0;
            }
            else
            {
                float multiplier = points / pointsNeeded;

                movementManager.AddTurn(lastAngle * movementManager.maxTurn * multiplier);
                movementManager.AddSpeed(-maxSlow * averageDisance * multiplier);
                points = 0;
            }
        }
        // if (points >= pointsNeeded)
        // {
        //     // movementManager.AddSpeed(-((maxDistance/2 - fowardDistance) / maxDistance/2) * maxSlow);
        //     movementManager.AddSpeed(-maxSlow * averageDisance);
        //     movementManager.AddTurn(lastAngle * movementManager.maxTurn);
        //
        //     points -= pointsNeeded;
        // }
    }
    
    private int CalculatePointsNeeded()
    {
        // Debug.Log("cal" + lastAngle);
        return Mathf.RoundToInt(Mathf.Abs(lastAngle * 5) + averageDisance * 3);
        return Mathf.RoundToInt(Mathf.Abs(lastAngle * 5));
    }
}
