using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Baddie : MonoBehaviour
{
    public NavMeshPather pather;
    public Vector3[] goodLocations;
    
    // Start is called before the first frame update
    public void OnEnable()
    {
        if (pather == null)
        {
            pather = FindObjectOfType<NavMeshPather>();
        }
        pather.OnPathFinish += OnOnPathFinish;
    }

    public void OnDisable()
    {
        pather.OnPathFinish -= OnOnPathFinish;
    }


    private void OnOnPathFinish()
    {
        if (goodLocations.Length > 0)
        {
            transform.position = goodLocations[Random.Range(0, goodLocations.Length)];
        }
    }
}
