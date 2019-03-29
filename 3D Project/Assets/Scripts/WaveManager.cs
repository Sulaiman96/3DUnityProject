using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject zombie;
    public float initialSpawnTime = 3f;
    public float nextSpawnTime = 10f;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombies", initialSpawnTime, nextSpawnTime);
    }

    void SpawnZombies()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
