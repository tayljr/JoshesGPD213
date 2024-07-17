using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float turnSpeed = 20f;

    private int randomOffset;
    // Start is called before the first frame update
    void Start()
    {
        randomOffset = Random.Range(-200, 200);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = Mathf.PerlinNoise1D(randomOffset + Time.time) * 2 - 1;
        gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, angle * turnSpeed, 0);
    }
}
