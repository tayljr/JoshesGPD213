using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Baddie : MonoBehaviour
{
    public NavMeshPather[] pathers;
    public SpawnPoint[] goodLocations;

    // Start is called before the first frame update
    public void OnEnable()
    {
        pathers = new []{FindObjectOfType<NavMeshPather>()};
        foreach (NavMeshPather pather in pathers)
        {

            pather.OnPathFinish += OnOnPathFinish;
        }
    }

    public void OnDisable()
    {
        foreach (NavMeshPather pather in pathers)
        {

            pather.OnPathFinish -= OnOnPathFinish;
        }
    }


    private void OnOnPathFinish()
    {
        goodLocations = FindObjectsOfType<SpawnPoint>();
        if (goodLocations.Length > 0)
        {
            transform.position = goodLocations[Random.Range(0, goodLocations.Length)].transform.position;
        }
    }
}