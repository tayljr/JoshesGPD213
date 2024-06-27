using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPather : SerializedMonoBehaviour
{
    // NAVMESH
// Variable that gets filled in with the points
    public NavMeshPath path;
    public Transform target;
    public Vector3 targetPoint;
    public TurnTowards turnTowards;
    private int currentCorner = 0;

// ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
    void Update()
    {
        // Create it in Awake or something
        path = new NavMeshPath();

        if (target != null)
        {
            targetPoint = target.position;
        }

        //todo is weird fix needed. WHY SPINN!!! i think cos distance is wierd. idk
        // Call this when you want to go somewhere! Then read the path variable and you’ll see
        NavMesh.CalculatePath(transform.position, targetPoint, NavMesh.AllAreas, path);
        if (turnTowards == null)
        {
            turnTowards = gameObject.GetComponentInParent<TurnTowards>();
        }

        float distance = Vector3.Distance(transform.position, path.corners[currentCorner]);
        if (distance <= 0.1f)
        {
            currentCorner++;
            if (currentCorner >= path.corners.Length - 1)
            {
                currentCorner = 0;
            }
        }
        else
        {
            turnTowards.NewTarget(path.corners[currentCorner]);
        }
    }


    private void OnDrawGizmos()
    {
        if (path != null)
            for (int index = 0; index < path.corners.Length - 1; index++)
            {
                Gizmos.DrawLine(path.corners[index], path.corners[index + 1]);
            }
    }

    public void NewTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void NewTarget(Vector3 newTarget)
    {
        target = null;
        targetPoint = newTarget;
    }
}