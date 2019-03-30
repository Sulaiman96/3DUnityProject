using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public GameObject pickUps;
    public Transform[] spawnPoints;
    public float someDelay = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPickUps", 0, someDelay);
    }

    void SpawnPickUps()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(pickUps, spawnPoints[index].transform);
    }

}
