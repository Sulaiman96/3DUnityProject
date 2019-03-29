using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject text;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
            text.SetActive(true);
        }
    }
}
