using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinWalls : MonoBehaviour
{
    public GameObject wallPrefab;
    public int areaLength;
    public int areaWidth;
    public float offset = 1f;
    public float wavelength = 2f;
    public float amplitude = 10f;
    public float threshold = 0.5f;
    public bool update = false;
    private List<GameObject> spawnedCubes = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < areaLength; x++)
        {
            for (int z = 0; z < areaWidth; z++)
            {
                var y = Mathf.PerlinNoise(x / wavelength + offset, z / wavelength + offset) * amplitude;
                if(y > threshold)
                {
                    spawnedCubes.Add(Instantiate(wallPrefab, new Vector3(x, 1, z), Quaternion.identity));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!update) return;

        if (spawnedCubes != null)
        {
            foreach (GameObject cube in spawnedCubes)
            {
                Destroy(cube);
            }

            spawnedCubes.Clear();
        }
        
        for (int x = 0; x < areaLength; x++)
        {
            for (int z = 0; z < areaWidth; z++)
            {
                var y = Mathf.PerlinNoise(x / wavelength + offset, z / wavelength + offset) * amplitude;
                if(y > threshold)
                {
                    spawnedCubes.Add(Instantiate(wallPrefab, new Vector3(x, 1, z), Quaternion.identity));
                }
            }
        }
    }

}
