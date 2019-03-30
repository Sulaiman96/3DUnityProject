using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Vector3 offset;
    public Transform targetToLookAt;
    public Transform chest;
    public Transform barrelEnd;

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButton("Aim") || Input.GetButton("Fire1"))
        {
            chest.LookAt(targetToLookAt.position);
            chest.rotation = chest.rotation * Quaternion.Euler(offset);
        }

    }
}
