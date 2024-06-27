using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnTowards : MonoBehaviour
{
    public float turnSpeed = 1f;
    public Transform target;
    private Vector3 targetDir;
    public Vector3 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            targetPos = target.position;
        }
        targetDir = targetPos - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
        //Debug.Log(angle);
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, angle * turnSpeed, 0);
    }

    public void NewTarget(Vector3 newTarget)
    {
        target = null;
        targetPos = newTarget;
    }
}
