using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class Vision : SerializedMonoBehaviour
{
    public Memory memory;
    public float maxAngle = 100;
    public int rays = 10;
    public float viewDistance = 10;
    public Dictionary<GameObject, Vector3> thingsSeen = new Dictionary<GameObject, Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (memory == null)
        {
            memory = gameObject.GetComponentInChildren<Memory>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float currentAngle = -maxAngle / 2f;
        
        for (int i = 0; i < rays; i++)
        {
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * transform.forward;
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, viewDistance);
            //add hit object to list
            Transform hitTransform = hit.transform;
            if (hitTransform != null)
            {
                if (thingsSeen.ContainsKey(hitTransform.gameObject))
                {
                    thingsSeen.Remove(hitTransform.gameObject);
                }
                thingsSeen.Add(hitTransform.gameObject, hitTransform.position);
            }
            float spreadAngle = maxAngle / (rays - 1);
            currentAngle += spreadAngle;
        }
        memory.NewMemory(thingsSeen);
        thingsSeen.Clear();
    }
}
