using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RusherController : EnemyController
{
    [SerializeField]
    private TrailRenderer tr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        tr = GetComponent<TrailRenderer>();
        tr.emitting = true;
        health = 3 + GameManager.Instance.extraHealth;
    }
}
