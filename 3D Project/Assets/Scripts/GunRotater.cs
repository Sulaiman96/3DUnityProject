using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotater : MonoBehaviour
{
    public float rotationSpeed = 40f;
    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed,0));
    }

}
