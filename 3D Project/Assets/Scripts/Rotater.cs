using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float rotationSpeed = 40f;
    private void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * rotationSpeed, Time.deltaTime * rotationSpeed, 0));
    }
}
