using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeScript : MonoBehaviour
{
    public bool dealsDamage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (dealsDamage)
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(3);
            }
            other.gameObject.GetComponent<EnemyController>().Freeze();
        }
    }
}
