using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
    public float maxAngle = 100;
    public int rays = 10;
    public float turnSpeed = 1f;
    public float turnAmount = 0f;
    public float maxDistance = 50f;
    public Gradient rayGradient;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //new system
        
        float currentAngle = -maxAngle / 2f;
        float averageDistance = 0f;
        Vector3 reflectionAverage = Vector3.zero;
        int hitRays = 0;
        for (int i = 0; i < rays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                hitRays++;
                Vector3 reflection = Vector3.Reflect(direction, hit.normal);
                Debug.DrawRay(hit.point,reflection * (maxDistance - hit.distance), rayGradient.Evaluate(hit.distance/maxDistance)); 
                averageDistance += hit.distance;
                reflectionAverage += reflection * (maxDistance - hit.distance);
                
                // Vector3 localReflection = transform.InverseTransformDirection(reflection);
                // Vector3 angle = Quaternion.Euler(localReflection) * Vector3.back;
                // gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, angle.y  * (maxDistance - hit.distance) * turnSpeed, 0);
                
            }
            float spreadAngle = maxAngle / (rays - 1);
            currentAngle += spreadAngle;
        }
        if (hitRays > 0)
        {
            reflectionAverage /= hitRays;
            averageDistance /= hitRays;
            Debug.DrawRay(transform.position, reflectionAverage, Color.red);
            Vector3 localReflection = transform.InverseTransformDirection(reflectionAverage);
            //todo why shake!!!
            turnAmount = Vector3.SignedAngle(transform.forward, reflectionAverage, Vector3.up);
            // Vector3 angle = Vector3.zero;
            // if (localReflection != Vector3.zero)
            // {
            //     angle = Quaternion.Euler(localReflection) * Vector3.back;
            // }

            //todo make turning more better
            //turnAmount += (1 - averageDistance / maxDistance) * angle.normalized.y;

            // turnAmount += angle.y;
            // Debug.Log(turnAmount);
        }
        //gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, angle.y * turnSpeed, 0);
        //gameObject.GetComponent<Rigidbody>().AddRelativeTorque(localReflection);
        
        //OLD 2 ray system
        
        // Vector3 left = Quaternion.Euler(0, -25, 0) * transform.forward;
        // RaycastHit hitLeft;
        // Physics.Raycast(transform.position, left, out hitLeft, maxDistance);
        // if (hitLeft.transform != null)
        // {
        //     //Debug.Log("left: " + hitLeft.distance);
        //     turnAmount = turnAmount + (1 - hitLeft.distance / maxDistance);
        //     Debug.DrawRay(transform.position, left * maxDistance, rayGradient.Evaluate(hitLeft.distance/maxDistance));
        // }
        // Vector3 right = Quaternion.Euler(0, 25, 0) * transform.forward;
        // RaycastHit hitRight;
        // Physics.Raycast(transform.position, right, out hitRight, maxDistance);
        // if (hitRight.transform != null)
        // {
        //     //Debug.Log("right: " + hitRight.distance);
        //     turnAmount = turnAmount - (1 - hitRight.distance / maxDistance);
        //     Debug.DrawRay(transform.position, right * maxDistance, rayGradient.Evaluate(hitRight.distance/maxDistance));
        // }

        //emergancy ray
        RaycastHit hitForward;
        
        if (Physics.Raycast(transform.position, transform.forward, out hitForward, 5))
        {
            turnAmount = 1 - hitForward.distance/5;
            Debug.DrawRay(transform.position, transform.forward * 5f, rayGradient.Evaluate(hitForward.distance/5));
        }
        
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, turnAmount * turnSpeed, 0);
        //Debug.Log(turnAmount);
        turnAmount = 0;
    }
}
