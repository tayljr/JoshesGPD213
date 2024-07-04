using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestSpawner : MonoBehaviour
{
    public int aiNumber = 1;

    public GameObject aiPrefab;

    
    
    public Transform target;

    public SpawnPoint[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        spawns = FindObjectsOfType<SpawnPoint>();
        for (int i = 0; i < aiNumber; i++)
        {
            GameObject newAi = Instantiate(aiPrefab, spawns[Random.Range(0, spawns.Length)].transform);
            if (target != null)
            {
                newAi.GetComponent<NavMeshPather>().target = target;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}