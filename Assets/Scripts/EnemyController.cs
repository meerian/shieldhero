using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    internal float movementSpeed;
    [SerializeField]
    internal Rigidbody2D rb;
    [SerializeField]
    internal Animator anim;
    [SerializeField]
    internal ParticleSystem ps;

    internal Vector3 knockback;

    internal bool isAlive = true;

    public float health;

    private bool isBurning = false;
    private bool isFreezing = false;
    private bool canTakeDashDmg = true;

    public GameObject expPrefab;
    public GameObject burnPrefab;
    public GameObject freezePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = GetComponent<ParticleSystem>();
        health = 5 + GameManager.Instance.extraHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && health <= 0)
        {
            isAlive = false;
            StartCoroutine("Dead");
        }
    }

    private void FixedUpdate() 
    {
        if (isAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.player.transform.position, movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += knockback * GameManager.Instance.knockbackPower * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shield")
        {
            knockback = transform.position - GameManager.Instance.player.transform.position;
            knockback.Normalize();
            transform.position += knockback * GameManager.Instance.knockbackPower;
        }

        else if (isAlive && other.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerController>().Hurt();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shield")
        {
            transform.position += knockback * GameManager.Instance.knockbackPower;
        }
                
        else if (isAlive && other.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerController>().Hurt();
        }
    }

    public void TakeDamage(float damage, bool isDash = false)
    {
        if (!isDash)
        {
            AudioManager.Instance.Play("enemyhurt");
            anim.SetTrigger("hurt");
            health -= damage;
        }
        else
        {
            if (canTakeDashDmg)
            {
                StartCoroutine("ResetDashDmg");
                canTakeDashDmg = false;
                AudioManager.Instance.Play("enemyhurt");
                anim.SetTrigger("hurt");
                health -= damage;
            }
        }
    } 

    public void Burn()
    {
        if (!isBurning)
        {
            isBurning = true;
            GameObject burn = Instantiate(burnPrefab, transform.position, Quaternion.identity);
            burn.transform.parent = transform;
            StartCoroutine("BurnEffect");
        }
    }

    public void Freeze()
    {
        if (!isFreezing)
        {
            isFreezing = true;
            GameObject freeze = Instantiate(freezePrefab, transform.position, Quaternion.identity);
            freeze.transform.parent = transform;
            movementSpeed = movementSpeed * 0.5f;
        }
    }

    IEnumerator BurnEffect()
    {
        yield return new WaitForSeconds(0.75f);
        TakeDamage(GameManager.Instance.player.GetComponent<PlayerController>().burnDamage);
        StartCoroutine("BurnEffect");
    }

    IEnumerator ResetDashDmg()
    {
        yield return new WaitForSeconds(0.3f);
        canTakeDashDmg = true;
    }

    IEnumerator Dead()
    {
        ps.Play();
        yield return new WaitForSeconds(0.5f);
        ps.Stop();
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
