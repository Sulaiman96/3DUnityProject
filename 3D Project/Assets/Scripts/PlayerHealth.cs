using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image screenFlash;
    public AudioClip deathClip;
    public float damageFlashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerController playerController;

    bool isDead;
    bool damaged;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            screenFlash.color = flashColour;
        }
        else
        {
            screenFlash.color = Color.Lerp(screenFlash.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int damageAmount)
    {
        damaged = true;
        currentHealth -= damageAmount;
        healthSlider.value = currentHealth;
        playerAudio.Play();
        if(currentHealth <=0 && !isDead)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;

        anim.SetTrigger("Die");
        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerController.enabled = false;
    }
}
