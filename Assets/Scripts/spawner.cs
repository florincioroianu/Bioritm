using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public float spawnRate = .3f;
    public GameObject hexagonPrefab;
    float nextTimeToSpawn = 0f;
    int number = 0;

    void Update()
    {
        if(Time.time>=nextTimeToSpawn)
        {
            GameObject hexagon = Instantiate(hexagonPrefab, Vector3.zero, Quaternion.identity);
            number++;
            nextTimeToSpawn = Time.time + 1f / spawnRate;
            if (number % 5 == 0)
                spawnRate += .1f;
        }
    }
}
