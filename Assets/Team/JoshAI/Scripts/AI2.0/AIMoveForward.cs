using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveForward : MonoBehaviour, IAIBehaviour
{
    public AIMovementManager movementManager;
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
    }

    public behaviourEnum Priority => behaviourEnum.moveForward;
    public void Execute(ref int points)
    {
        movementManager.AddSpeed(movementManager.maxSpeed);
    }
}
