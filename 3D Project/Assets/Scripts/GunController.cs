using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public int damagePerBullet = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 20f;
    public Camera fpsCam;
    public Transform barrelEnd;
    public Transform chest;
    public Vector3 offset;
    public Transform targetToLookAt;
    int shootable;
    private float nextTimeToFire = 0f;
    private AudioSource gunAudio;

    private void Start()
    {
        shootable = LayerMask.GetMask("Shootable");
        gunAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {
            chest.LookAt(targetToLookAt.position);
            chest.rotation = chest.rotation * Quaternion.Euler(offset);
            nextTimeToFire = Time.time + (1 / fireRate);
            Shoot();
        }
    }

    private void Shoot()
    {
        gunAudio.Play();

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, shootable))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damagePerBullet, hit.point);
            }
        }

    }
}
