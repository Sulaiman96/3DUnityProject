using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private int wave = 0;
    private int numOfTotalWaves = 10;
    private int numOfEnemyTypes = 3;
    private int[,] waveSpawnCount; 
    private int aliveEnemies = 0; // Will count how many enemies are currently alive, MAKE SURE TO HAVE THIS DECREASE WHEN ONE DIES

    void Start()
    {
        waveSpawnCount = new int[numOfTotalWaves, numOfEnemyTypes];
        //Set Wave 0 to 0 spawns because we dont want anything to spawn in wave 0.
        waveSpawnCount[0, 0] = 0;
        waveSpawnCount[0, 1] = 0;
        waveSpawnCount[0, 2] = 0;

        waveSpawnCount[1, 0] = 10;    //Store that we want 10 of Enemy 0 to spawn in Wave 1
        waveSpawnCount[1, 1] = 0;
        waveSpawnCount[1, 2] = 0;

        /*
        ...
        And So On
        ...
        */
    }

    void Update()
    {
        if (aliveEnemies <= 0)
        {
            if (wave <= numOfTotalWaves)
            {
                wave++;
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        for (int enemyType = 0; enemyType != numOfEnemyTypes; enemyType++) // For every enemy Type
        {
            for (int i = 0; i < waveSpawnCount[wave, enemyType]; i++) // and for every number of units to spawn for that enemy type
            {
                SpawnEnemy(enemyType); // spawn that enemy type
                aliveEnemies++; // Add one to our alive enemies counter
            }
        }
    }

    private void SpawnEnemy(int enemyType)
    {
        switch (enemyType)
        {
            case 0:
                // Do whatever you do to load enemy 0
                break;
            case 1:
                // Do whatever you do to load enemy 1
                break;
            case 2:
                // Do whatever you do to load enemy 2
                break;
        }
    }
}
