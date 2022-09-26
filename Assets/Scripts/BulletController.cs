using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 dir;
    public float movementSpeed;
    public bool hitenemy = false;

    private void FixedUpdate()
    {
        transform.position += dir * movementSpeed * Time.deltaTime;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Shield")
        {
            AudioManager.Instance.Play("enemyhurt");
            if (other.gameObject.GetComponent<ShieldController>().deflectBullet)
            {
                dir = - dir;
                hitenemy = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerController>().Hurt();
        }
        if (hitenemy && other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(5.0f);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
