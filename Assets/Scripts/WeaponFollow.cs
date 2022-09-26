using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public float offset = 0;

    private void Update ()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();

        // Vector2 scale = transform.localScale;
        // if (difference.x < 0)
        // {
        //     //scale.y = -1;
        // }
        // else
        // {
        //     //scale.y = 1;
        // }
        // transform.localScale = scale;

        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);

        // if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        // {
        //     renderer.sortingOrder = -1;
        // }
        // else
        // {
        //     renderer.sortingOrder = 0;
        // }
    }
}
