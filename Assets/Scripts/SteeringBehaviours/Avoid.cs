using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{
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
        //todo something ain't right here
        Vector3 left = Quaternion.Euler(0, -25, 0) * transform.forward;
        RaycastHit hitLeft;
        Physics.Raycast(transform.position, left, out hitLeft, maxDistance);
        if (hitLeft.transform != null)
        {
            turnAmount =+ hitLeft.distance/maxDistance;
            Debug.DrawRay(transform.position, left * maxDistance, rayGradient.Evaluate(hitLeft.distance/maxDistance));
        }
        Vector3 right = Quaternion.Euler(0, 25, 0) * transform.forward;
        RaycastHit hitRight;
        Physics.Raycast(transform.position, right, out hitRight, maxDistance);
        if (hitRight.transform != null)
        {
            turnAmount =- hitRight.distance/maxDistance;
            Debug.DrawRay(transform.position, right * maxDistance, rayGradient.Evaluate(hitRight.distance/maxDistance));
        }

        RaycastHit hitForward;
        Physics.Raycast(transform.position, transform.forward, out hitForward, 5);
        if (hitForward.transform != null)
        {
            turnAmount =- hitForward.distance/5;
            Debug.DrawRay(transform.position, transform.forward * 5f, rayGradient.Evaluate(hitForward.distance/5));
        }
        
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, turnAmount * turnSpeed, 0);
    }
}
