using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region Wave Class
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform monster;
        public int count;
        public float spawnRate;
    }
    #endregion

    #region Variables
    public enum SpawnState { Spawning, Waiting, Counting };
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float countDownBetweenWaves = 5f;
    public float waveCountDown; //change to private after prototype demo!
    public SpawnState currentState = SpawnState.Counting;

    private int waveIndex = 0;
    private float waitBeforeSearching = 1f;
    #endregion

    private void Start()
    {
        waveCountDown = countDownBetweenWaves;
    }

    private void Update()
    {
        //make sure to not spawn waves until the previous wave has been dealt with.
        if (currentState == SpawnState.Waiting)
        {
            if (!MonstersStillAlive())
            {
                BeginNewRound();
                //begin a new round!
                SpawnWave(waves[waveIndex]);
            }
            else
            {
                return;
            }
        }


        if (waveCountDown <= 0)
        {
            if(currentState != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[waveIndex]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    #region Functions
    //allows for a wait.
    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.waveName);
        currentState = SpawnState.Spawning;

        for(int i = 0; i < wave.count; i++)
        {
            SpawnMonsters(wave.monster);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        currentState = SpawnState.Waiting; //put the player in waiting mode so that new waves doesn't spawn until all monsters have been killed off.
        yield break;
    }

    //spawns monsters
    private void SpawnMonsters(Transform monster)
    {
        Debug.Log("Spawning monsters ... " + monster.name);
        Transform randomSpawnSelector = spawnPoints[Random.Range(0, spawnPoints.Length)];//choose a random spawn point.
        Instantiate(monster, randomSpawnSelector.position, randomSpawnSelector.rotation);
    }

    //check to see if any monsters (objects with the tag monster) is still present in the game.
    private bool MonstersStillAlive()
    {
        waitBeforeSearching -= Time.deltaTime;
        if (waitBeforeSearching <= 0)
        {
            waitBeforeSearching = 1f; 
            if (GameObject.FindGameObjectWithTag("Monster") == null)
            {
                return false;
            }
        }
        return true;
    }

    //responsible for starting a new round of waves once all the monsters in the previous wave has been killed off. 
    private void BeginNewRound()
    {
        Debug.Log("Wave Completed");
        currentState = SpawnState.Counting;
        waveCountDown = countDownBetweenWaves;
        if (waveIndex + 1 > waves.Length - 1)
        {
            waveIndex = 0;
            Debug.Log("All waves complete");
            Debug.Log("Starting again");
            //I could add stat multiplyer or increase number of enemies here.
        }
        else
        {
            waveIndex++;
        }

    }
    #endregion
}
