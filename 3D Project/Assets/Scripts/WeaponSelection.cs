using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Asset/New Weapon")]
public class WeaponSelection : ScriptableObject
{
    public GameObject prefab;
    public WEAPONTYPE weaponType;
    public RuntimeAnimatorController animatorOverideController;

    public enum WEAPONTYPE
    {
        SWORD,
        BOW,
        GUN,
        RPG
    }
}
