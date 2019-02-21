using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Asset/New Weapon")]
public class WeaponSelection : ScriptableObject
{
    public GameObject prefab; 
    public WEAPONTYPE weaponType;
    public RuntimeAnimatorController animatorOverideController;

    public GameObject projectile; //what we will be shooting (different weapons may have different projectiles)
    public float projectileForce; //How much force will this projectile have, crossbow may shoot a lot harder than a bow.

    public enum WEAPONTYPE
    {
        SWORD,
        BOW,
        GUN,
        RPG
    }
}
