using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Avoid : MonoBehaviour
{
    public float maxAngle = 100;
    public int rays = 10;
    public float turnSpeed = 1f;
    private float angle = 0f;
    public float maxDistance = 50f;
    public Gradient rayGradient;

    public float maxBreak = 30f;
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
                reflectionAverage += reflection;
                
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
            reflectionAverage = reflectionAverage.normalized;
            averageDistance /= hitRays;
            averageDistance = maxDistance - averageDistance;
            Debug.DrawRay(transform.position, reflectionAverage * averageDistance, Color.red);
            averageDistance /= maxDistance;
            //averageDistance = Mathf.InverseLerp(0, maxDistance, averageDistance);
            Vector3 localReflection = transform.InverseTransformDirection(reflectionAverage);
            //todo why shake!!!
            //angle += Vector3.SignedAngle(transform.forward, reflectionAverage, Vector3.up);
            // Vector3 angle = Vector3.zero;
            float angle = Vector3.Angle(transform.forward, reflectionAverage);
            if (reflectionAverage != Vector3.zero)
            {
            }

            //todo make turning more better
            Debug.Log(averageDistance);
            this.angle += angle * averageDistance;

            // angle += angle.y;
            // Debug.Log(angle);
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
        //     angle = angle + (1 - hitLeft.distance / maxDistance);
        //     Debug.DrawRay(transform.position, left * maxDistance, rayGradient.Evaluate(hitLeft.distance/maxDistance));
        // }
        // Vector3 right = Quaternion.Euler(0, 25, 0) * transform.forward;
        // RaycastHit hitRight;
        // Physics.Raycast(transform.position, right, out hitRight, maxDistance);
        // if (hitRight.transform != null)
        // {
        //     //Debug.Log("right: " + hitRight.distance);
        //     angle = angle - (1 - hitRight.distance / maxDistance);
        //     Debug.DrawRay(transform.position, right * maxDistance, rayGradient.Evaluate(hitRight.distance/maxDistance));
        // }

        //emergancy ray
        RaycastHit hitForward;
        
        if (Physics.Raycast(transform.position, transform.forward, out hitForward, 5))
        {
            angle = 1 - hitForward.distance/5;
            Debug.DrawRay(transform.position, transform.forward * 5f, rayGradient.Evaluate(hitForward.distance/5));
        }
        
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, angle * turnSpeed, 0);
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * (maxBreak * averageDistance));
        //Debug.Log(angle);
        angle = 0;
    }
}
