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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWeaponEquipped) return; //if weapon isn't equipped, return.

        #region BOW
        if(weapon.weaponType == WeaponSelection.WEAPONTYPE.BOW)
        {
            //will first check if player is aiming and set the bool for animation to trigger
            anim.SetBool("isAiming", Input.GetButton("Aim"));

            //attack if arrow is drawn and player clicks attack.
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Aim") && Input.GetButtonDown("Attack")) ;
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
        }
    }
}
