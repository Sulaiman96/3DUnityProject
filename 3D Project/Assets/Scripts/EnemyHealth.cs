using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 30;
    public int currentHealth;
    public float deathSinkSpeed = 0.5f;
    public int moneyPerKill = 100;
    public int totalKills;
    public int totalMoneyEarned;
    public AudioClip deathClip;
    public ParticleSystem hitParticles;

    private Animator anim;
    private AudioSource monsterAudio;
    private CapsuleCollider capsuleCollider;
    private bool isDead;
    private bool isSinking;


    void Start()
    {
        anim = GetComponent<Animator>();
        monsterAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * deathSinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        monsterAudio.Play();
        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if(currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        monsterAudio.clip = deathClip;
        monsterAudio.Play();
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        MoneyManager.money += moneyPerKill;
        Destroy(gameObject, 2f);
    }
}
