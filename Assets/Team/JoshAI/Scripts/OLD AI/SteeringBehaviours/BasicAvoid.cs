using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAvoid : MonoBehaviour
{
    public float maxAngle = 100;
    public int rays = 10;
    public float turnSpeed = 1f;
    public float maxDistance = 50f;
    public float emergancyDistance = 5f;
    public float emergencyTurn = 70;
    public float maxSlow = 50f;
    
    private float angle = 0f; 
    
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentAngle = -maxAngle / 2f;
        //float averageDistance = 0f;
        Vector3 reflectionAverage = Vector3.zero;
        int hitRays = 0;
        for (int i = 0; i < rays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                hitRays++;
                if (currentAngle > 0)
                {
                    angle -= maxDistance - hit.distance / maxDistance;
                }

                if (currentAngle < 0)
                {
                    angle += maxDistance - hit.distance / maxDistance;
                }

                //angle *= (maxDistance - hit.distance) / maxDistance;
            }

            float spreadAngle = maxAngle / (rays - 1);
            currentAngle += spreadAngle;
        }

        if (hitRays > 0)
        {
            //angle /= hitRays;
        }

        //emergency ray
        RaycastHit hitForward;

        if (Physics.Raycast(transform.position, transform.forward, out hitForward, maxDistance/2))
        {
            if (hitForward.distance <= emergancyDistance)
            {
                angle = emergencyTurn;
            }
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * (((maxDistance/2 - hitForward.distance) / maxDistance/2) * maxSlow));
        }
        //Debug.Log(angle);
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, angle * turnSpeed, 0);
        angle = 0;
    }
}