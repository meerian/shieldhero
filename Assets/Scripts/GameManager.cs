using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float knockbackPower;

    public float exp = 0;

    public float maxexp = 5;

    public float level = 0;

    public int timeTaken = 0;

    public GameObject player;

    public GameObject shield;

    public float extraHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        StartCoroutine("Timer");
    }

    // Update is called once per frame
    void Update()
    {
        if (maxexp <= exp)
        {
            level++;
            maxexp = 5 + 5 * level;
            exp = 0;
            AudioManager.Instance.Play("levelup");
            UpgradeManager.Instance.ChooseUpgrades();
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            TimeCount();
            yield return new WaitForSeconds(1);
        }
    }

    private void TimeCount()
    {
        timeTaken++;
    }
}
