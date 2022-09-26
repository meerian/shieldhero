using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private Rigidbody2D spriteRb;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private TrailRenderer tr;
    [SerializeField]
    private float dashSpeed = 10f;
    [SerializeField]
    private float dashTime = 0.5f;
    public bool isDashing = false;
    private bool canDash = true;
    private float dashCooldown = 1f;
    private bool canHurt = true;

    public float prevDash;
    public int health;
    public GameObject light;

    public float burnDamage = 0;
    public GameObject burnPrefab;
    public float burnCooldown = 0.5f;

    public GameObject freezeCircle;
    public float freezeCooldown = 4f;

    public GameObject shieldPrefab;

    [SerializeField]
    private Animator anim;

    private Vector3 movement;
    private Vector2 mousePosition;
    private float angle;

    // Start is called before the first frame update
    private void Start()
    {
        Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        tr.emitting = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
        {
            Dead();
            return;
        }
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        anim.SetFloat("moving", movement.magnitude);
        if (canDash && Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            StartCoroutine("Dash");
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Vector3 dashpos = new Vector3(mousePosition.x, mousePosition.y, 0f) - transform.position;
            dashpos.Normalize();
            transform.position += dashpos * Time.fixedDeltaTime * dashSpeed;
        }
        else
        {
            transform.position += movement * Time.fixedDeltaTime * movementSpeed;
        }
    }

    public void AddExp()
    {
        AudioManager.Instance.Play("getexp");
        GameManager.Instance.exp++;
    }

    public void DashUpgrade()
    {
        canDash = false;
        isDashing = true;
        dashSpeed = 2f;
    }

    public void ShieldUpgrade()
    {
        GameObject shield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        shield.GetComponent<WeaponFollow>().offset = 180;
        shield.transform.parent = transform;
    }

    public void UpgradeBurn(int lvl)
    {
        switch (lvl)
        {
            case 0:
                StartCoroutine("BurnEffect");
                burnDamage++;
                break;
            case 2:
                movementSpeed = 1.5f;
                burnCooldown = 0.1f;
                break;
            default:
                burnDamage++;
                break;
        }
    }

    public void UpgradeFreeze(int lvl)
    {
        switch (lvl)
        {
            case 0:
                StartCoroutine("FreezeEffect");
                break;
            case 2:
                freezeCooldown = 2f;
                freezeCircle.GetComponent<FreezeScript>().dealsDamage = true;
                break;
            default:
                freezeCircle.transform.localScale += new Vector3(0.2f, 0.2f, 0f);
                break;
        }
    }

    public void Hurt()
    {
        if (canHurt)
        {
            AudioManager.Instance.Play("playerhurt");
            UIManager.Instance.FlashHurt();
            CameraController.Instance.ShakeCamera(5f, 0.2f);
            health--;
            StartCoroutine("Invincibility");
        }
    }

    public void Dead()
    {
        light.SetActive(false);
        UIManager.Instance.GameOver();
    }

    IEnumerator Invincibility()
    {
        canHurt = false;
        yield return new WaitForSeconds(0.5f);
        canHurt = true;
    }

    IEnumerator Dash()
    {
        AudioManager.Instance.Play("dash");
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        prevDash = Time.time;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    IEnumerator BurnEffect()
    {
        yield return new WaitForSeconds(burnCooldown);
        AudioManager.Instance.Play("spawnfire");
        Instantiate(burnPrefab, transform.position, Quaternion.identity);
        StartCoroutine("BurnEffect");
    }

    IEnumerator FreezeEffect()
    {
        AudioManager.Instance.Play("freezecircle");
        freezeCircle.SetActive(true);
        yield return new WaitForSeconds(1f);
        freezeCircle.SetActive(false);
        yield return new WaitForSeconds(freezeCooldown);
        StartCoroutine("FreezeEffect");
    }
}
