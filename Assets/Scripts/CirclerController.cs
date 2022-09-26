using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclerController : EnemyController
{
    [SerializeField]
    private float dashSpeed = 5f;
    [SerializeField]
    private float returnSpeed = 2f;
    [SerializeField]
    private float dashDuration = 0.5f;
    [SerializeField]
    private float Radius = 0.1f;
    [SerializeField]
    private TrailRenderer tr;

 
    //private Vector2 _centre;
    [SerializeField]
    private float angle;

    private bool isDashing = false;

    private bool isCircling = false;

    private bool isCharging = false;

    private Vector3 dashDir;

    private Vector3 moveTowardsVec;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        tr = GetComponent<TrailRenderer>();
        anim = GetComponent<Animator>();
        health = 5 + GameManager.Instance.extraHealth;
        StartCoroutine("AttackPattern");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCharging)
        {
            return;
        }
        moveTowardsVec = GameManager.Instance.player.transform.position + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * Radius;
        if (isDashing)
        {
            transform.position += dashDir * dashSpeed * Time.deltaTime;
        }
        else if (!isCircling && Vector3.Distance(moveTowardsVec, transform.position) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTowardsVec, returnSpeed * Time.deltaTime);
        }
        else
        {
            isCircling = true;
            angle += movementSpeed * Time.deltaTime;
            var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * Radius;
            transform.position = GameManager.Instance.player.transform.position + offset;            
        }

    }

    IEnumerator AttackPattern()
    {
        while (!isCircling)
        {
            yield return new WaitForSeconds(1.0f);
        }
        yield return new WaitForSeconds(4.0f);
        dashDir = GameManager.Instance.player.transform.position - transform.position;
        tr.emitting = true;
        isCircling = false;
        isCharging = true;
        anim.SetBool("charging", true);
        yield return new WaitForSeconds(1.0f);
        isCharging = false;
        anim.SetBool("charging", false);
        isDashing = true;
        AudioManager.Instance.Play("circlerdash");
        yield return new WaitForSeconds(dashDuration);
        angle += 4;
        moveTowardsVec = GameManager.Instance.player.transform.position + new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * Radius;
        tr.emitting = false;
        isDashing = false;
        StartCoroutine("AttackPattern");
    }
}
