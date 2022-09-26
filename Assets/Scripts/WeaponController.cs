using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    private float fireRate = 1f;

    private float nextFire = 0.0f;

    private bool mouseDown;

    private void Update() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mouseDown = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            mouseDown = false;
        }

        if (mouseDown && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
        }
    }
}
