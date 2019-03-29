using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    private void Update()
    {
        Destroy(this.gameObject, 1f);
    }
}
