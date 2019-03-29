using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool isWeaponEquipped;
    public WeaponSelection weapon;
    public Transform chestBone;
    public Vector3 lookOffset;
    public Transform targetToLookAt;

    public Transform weaponSpwanPoint;
    public GameObject weaponToEquip;
    WeaponActivator wp;

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        //if there is a weapon, put on the correct overide controller for the animation to play out.
        if(weapon != null)
        {
            anim.runtimeAnimatorController = weapon.animatorOverideController;
            anim.SetLayerWeight(1, 1);
            isWeaponEquipped = true;

            weaponToEquip = Instantiate(weapon.prefab, weaponSpwanPoint) as GameObject; //instantiate the new weapon object
            //weaponToEquip.transform.position = weaponSpwanPoint.position; //place it on weapon holder object which I put on my characters left hand.

            wp = weaponToEquip.GetComponent<WeaponActivator>();
            if(weapon.weaponType == WeaponSelection.WEAPONTYPE.BOW)
            {
                wp.projectile = weapon.projectile; //different bows will have different arrows (could be flames or freezing effect)
                wp.projectileForce = weapon.projectileForce; //different bows will have different force (for damage purposes)
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isWeaponEquipped) return; //if weapon isn't equipped, return.

        #region BOW
        if(weapon.weaponType == WeaponSelection.WEAPONTYPE.BOW)
        {
            //will first check if player is aiming and set the bool for animation to trigger
            anim.SetBool("isAiming", Input.GetButton("Aim"));

            //attack if arrow is drawn and player clicks attack.
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Aim") && Input.GetButtonDown("Attack"))
            {
                anim.SetTrigger("Attack");
                wp.AttackBow();
            }

        }
        #endregion
    }

    private void LateUpdate()
    {
        if (!isWeaponEquipped) return;

        if (Input.GetButton("Aim"))
        {
            //Look At Target
            chestBone.LookAt(targetToLookAt.position);

            //Set Player Chest Rotation
            chestBone.rotation = chestBone.rotation * Quaternion.Euler(lookOffset);

            //Set Emitter Look Point
            wp.emitPoint.LookAt(targetToLookAt);
        }
    }
}
