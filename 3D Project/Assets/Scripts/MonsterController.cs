using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{   
    private Transform player;

    private NavMeshAgent nav;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.currentHealth != 0)
        {
            nav.SetDestination(player.position);
        }
    }
}
