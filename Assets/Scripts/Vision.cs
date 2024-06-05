using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public float maxAngle = 100;
    public int rays = 10;
    public float viewDistance = 10;
    public List<GameObject> thingsSeen;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentAngle = -maxAngle / 2f;
        thingsSeen = new List<GameObject>();
        
        for (int i = 0; i < rays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, viewDistance);
            //add hit object to list
            float spreadAngle = maxAngle / (rays - 1);
            currentAngle += spreadAngle;
        }
    }
}
