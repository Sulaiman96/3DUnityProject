using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthAmount = 100;
    public int totalHealthPickupsAcquired;
    public AudioClip audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            HealThePlayer(other);
        }
    }

    void HealThePlayer(Collider player)
    {
        totalHealthPickupsAcquired++;
        player.GetComponent<PlayerHealth>().Healing(healthAmount);
        AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
        Destroy(gameObject);
    }
}
