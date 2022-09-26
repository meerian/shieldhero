using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject basicenemy;
    public GameObject rusher;
    public GameObject circler;
    public GameObject gunner;

    private float basicenemyCooldown = 2f;
    private float rusherCooldown = 10f;
    private float circlerCooldown = 15f;
    private float gunnerCooldown = 15f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartTimer");
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine("SpawnBasicEnemy");
        yield return new WaitForSeconds(20f);
        StartCoroutine("SpawnRusher");
        yield return new WaitForSeconds(10f);
        basicenemyCooldown = 1.5f;
        yield return new WaitForSeconds(10f);
        StartCoroutine("SpawnCircler");
        yield return new WaitForSeconds(20f);
        StartCoroutine("SpawnGunner");
        yield return new WaitForSeconds(10f);
        GameManager.Instance.extraHealth = 1;
        StartCoroutine("HealthLoop");
    }

    IEnumerator HealthLoop()
    {
        basicenemyCooldown *= 0.8f;
        rusherCooldown *= 0.8f;
        circlerCooldown *= 0.8f;
        gunnerCooldown *= 0.8f;
        yield return new WaitForSeconds(15f);
        GameManager.Instance.extraHealth++;
        StartCoroutine("HealthLoop");
    }
    IEnumerator SpawnBasicEnemy()
    {
        Vector3 point = Random.insideUnitCircle;
        point.Normalize();
        Instantiate(basicenemy, point + GameManager.Instance.player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(basicenemyCooldown);
        StartCoroutine("SpawnBasicEnemy");
    }

    IEnumerator SpawnRusher()
    {
        AudioManager.Instance.Play("spawnrusher");
        Vector3 point = Random.insideUnitCircle;
        point.Normalize();
        Instantiate(rusher, point * 1.5f + GameManager.Instance.player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(rusherCooldown);
        StartCoroutine("SpawnRusher");
    }

    IEnumerator SpawnCircler()
    {
        AudioManager.Instance.Play("spawncircler");
        Vector3 point = Random.insideUnitCircle;
        point.Normalize();
        Instantiate(circler, point * 1.5f + GameManager.Instance.player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(circlerCooldown);
        StartCoroutine("SpawnCircler");
    }

    IEnumerator SpawnGunner()
    {
        AudioManager.Instance.Play("spawngunner");
        Vector3 point = Random.insideUnitCircle;
        point.Normalize();
        Instantiate(gunner, point * 1.5f + GameManager.Instance.player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(gunnerCooldown);
        StartCoroutine("SpawnGunner");
    }
}
