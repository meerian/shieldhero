using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public float thornDamage = 0;
    public float dashDamage = 5;
    public bool healthScaling = false;

    public bool deflectBullet = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (thornDamage > 0)
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(thornDamage);
            }
            if (GameManager.Instance.player.GetComponent<PlayerController>().isDashing)
            {
                if (healthScaling)
                {
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(dashDamage + GameManager.Instance.player.GetComponent<PlayerController>().health, true);
                }
                else
                {
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(dashDamage, true);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (thornDamage > 0)
            {
                other.gameObject.GetComponent<EnemyController>().TakeDamage(thornDamage);
            }
            if (GameManager.Instance.player.GetComponent<PlayerController>().isDashing)
            {
                if (healthScaling)
                {
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(dashDamage + GameManager.Instance.player.GetComponent<PlayerController>().health, true);
                }
                else
                {
                    other.gameObject.GetComponent<EnemyController>().TakeDamage(dashDamage, true);
                }
            }
        }        
    }
}
