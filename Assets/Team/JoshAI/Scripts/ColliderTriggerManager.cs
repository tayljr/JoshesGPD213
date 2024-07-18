using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderTriggerManager : MonoBehaviour
{
    public delegate void CollisionDelegate(Collision other);
    public delegate void ColliderDelegate(Collider other);

    public event CollisionDelegate OnCollisionEnter_Event;
    public event CollisionDelegate OnCollisionExit_Event;
    public event CollisionDelegate OnCollisionStay_Event;
    
    public event ColliderDelegate OnTriggerEnter_Event;
    public event ColliderDelegate OnTriggerExit_Event;
    public event ColliderDelegate OnTriggerStay_Event;
    

    private void OnCollisionEnter(Collision other)
    {
        OnCollisionEnter_Event?.Invoke(other);
    }

    private void OnCollisionExit(Collision other)
    {
        OnCollisionExit_Event?.Invoke(other);    }

    private void OnCollisionStay(Collision other)
    {
        OnCollisionStay_Event?.Invoke(other);    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnter_Event?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExit_Event?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStay_Event?.Invoke(other);
    }
}
