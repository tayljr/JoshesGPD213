using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public enum behaviourEnum
{
    moveForward,
    avoid,
    neighbourCheck,
    seperation,
    alignment,
    cohesion,
    pather,
    turnTowards,
    wander,
}

public class AIMovementManager : SerializedMonoBehaviour
{
    public int totalPoints = 10;
    public List<IAIBehaviour> behaviours;
    public float maxSpeed = 30f;
    public float maxTurn = 1f;

    public float desiredSpeed = 0f;

    public float desiredTurn = 0f;

    public int remainingPoints;

    public List<GameObject> myNeighbours;
    // Start is called before the first frame update
    void Start()
    {
        behaviours = new List<IAIBehaviour>(GetComponents<IAIBehaviour>());
        behaviours.Sort((a, b) => a.Priority.CompareTo(b.Priority));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        remainingPoints = totalPoints;
        desiredSpeed = 0;
        desiredTurn = 0;

        foreach (var behaviour in behaviours)
        {
            behaviour.Execute(ref remainingPoints);

            if (remainingPoints <= 0)
            {
                break;
            }
        }
        if (desiredSpeed > maxSpeed)
        {
            desiredSpeed = maxSpeed;
        }
        else if (desiredSpeed < -maxSpeed)
        {
            desiredSpeed = -maxSpeed;
        }

        if (desiredTurn > maxTurn)
        {
            desiredTurn = maxTurn;
        }
        else if (desiredTurn < -maxTurn)
        {
            desiredTurn = -maxTurn;
        }

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * desiredSpeed);
        rb.AddRelativeTorque(0, desiredTurn, 0);
    }

    public void AddTurn(float amount)
    {
        desiredTurn += amount;
    }

    public void SetTurn(float amount)
    {
        desiredTurn = amount;
    }

    public void AddSpeed(float amount)
    {
        desiredSpeed += amount;
    }
    public void SetSpeed(float amount)
    {
        desiredSpeed = amount;
    }

    public void SetNeighbours(List<GameObject> newNeighbours)
    {
        myNeighbours = newNeighbours;
    }
}
