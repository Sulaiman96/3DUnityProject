using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivator : MonoBehaviour
{
    public Transform emitPoint;
    public GameObject projectile;
    public float projectileForce;

    public void AttackBow()
    {
        Quaternion offset = Quaternion.Euler(0, 0, -80); //adjust the rotation of the projectile, stuff like an arrow.

        GameObject proj = Instantiate(projectile);
        proj.transform.position = emitPoint.position; 
        proj.transform.rotation = emitPoint.rotation * offset;

        proj.GetComponent<Rigidbody>().AddForce(emitPoint.forward * projectileForce, ForceMode.Impulse); //shooting the projectile. Using impulse because I want the sudden force.
        Destroy(proj, 5); //Destroy the game object after 5 seconds (for now, once I set the monsters up, i will destroy the projectile on hit).
    }
}
