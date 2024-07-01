using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Vector3> targetPath;
    public TurnTowards turnTowards;
    private int currentCorner = 0;


    private void Start()
    {
        NewPath();
    }

    // ONLY USE UPDATE WHILE DEVELOPING. Eventually your planner will call this only when it needs to
    void Update()
    {
        if (targetPath.Count == 0)
        {
            NewPath();
        }
        else
        {
            if (target != null && (Vector3.Distance(targetPath.Last(), targetPoint) >= 1 ||
                                   Vector3.Distance(targetPath.Last(), target.position) >= 1))
            {
                NewPath();
            }

            if (turnTowards == null)
            {
                turnTowards = gameObject.GetComponentInParent<TurnTowards>();
            }


            float distance = Vector3.Distance(transform.position, targetPath[currentCorner]);
            if (distance <= 0.5f)
            {
                currentCorner++;
                if (currentCorner >= path.corners.Length)
                {
                    currentCorner = 0;

                    //cos it is cool 
                }
            }
            else
            {
                turnTowards.NewTarget(targetPath[currentCorner]);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (path != null)
            for (int index = 0; index < targetPath.Count - 1; index++)
            {
                Gizmos.DrawLine(targetPath[index], targetPath[index + 1]);
            }
    }

    public void NewTarget(Transform newTarget)
    {
        target = newTarget;
        NewPath();
    }

    public void NewTarget(Vector3 newTarget)
    {
        target = null;
        targetPoint = newTarget;
        NewPath();
    }

    private void NewPath()
    {
        path = new NavMeshPath();

        if (target != null)
        {
            targetPoint = target.position;
        }
        
        //todo is weird fix needed. WHY SPINN!!! i think cos distance is wierd. idk
        // Call this when you want to go somewhere! Then read the path variable and you’ll see
        NavMesh.CalculatePath(transform.position, targetPoint, NavMesh.AllAreas, path);
        targetPath = new List<Vector3>(path.corners);
        currentCorner = 0;
    }
}