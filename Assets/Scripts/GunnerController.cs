using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerController : EnemyController
{
    private bool inPosition = false;

    public GameObject bulletPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        health = 10 + GameManager.Instance.extraHealth;
    }

    void Update()
    {
        if (!inPosition && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= 0.8)
        {
            inPosition = true;
            StartCoroutine("Shoot");
        }
        if (isAlive && health <= 0)
        {
            isAlive = false;
            StartCoroutine("Dead");
        }
    }

    void FixedUpdate()
    {
        if (!inPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.player.transform.position, movementSpeed * Time.deltaTime);
        }
    }

    private void OnBecameInvisible()
    {
        inPosition = false;
    }

    IEnumerator Shoot()
    {
        AudioManager.Instance.Play("gunnershoot");
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BulletController>().dir = GameManager.Instance.player.transform.position - transform.position;
        yield return new WaitForSeconds(3.0f);
        if (inPosition)
        {
            StartCoroutine("Shoot");
        }
    }
}
