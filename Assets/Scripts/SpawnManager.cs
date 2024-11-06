using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] garbagePrefabs;
    private float spawnMaxX = 0;
    private float spawnMinX = -4.5f;
    private float startDelay = 2;
    private float spawnInterval = 3;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomGarbage", startDelay, spawnInterval);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomGarbage()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), 0, 0);
        int garbageIndex = Random.Range(0, garbagePrefabs.Length);
        Instantiate(garbagePrefabs[garbageIndex], spawnPos, garbagePrefabs[garbageIndex].transform.rotation);
    }
}
