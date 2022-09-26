using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    public int[] selectedUpgrades;
    public GameObject levelUI;
    public Image[] upgradeBox;
    public Animator anim;

    public List<Upgrade> upgrades;
    public GameObject[] selections;

    public Text upgradeText;

    public Sprite[] upgradeSprites;

    private int curSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        selectedUpgrades = new int[upgradeSprites.Length];
    }

    public void ShowDetail(int selection)
    {
        AudioManager.Instance.Play("mouseover");
        selections[curSelected].SetActive(false);
        selections[selection].SetActive(true);
        upgradeText.text = upgrades[selection].desc;
        curSelected = selection;
    }

    public void ChooseUpgrades()
    {
        Time.timeScale = 0;
        levelUI.SetActive(true);
        anim.SetTrigger("enter");
        upgrades = new List<Upgrade>();
        Stack<int> temp = new Stack<int>();
        while (temp.Count < 3)
        {
            int chosen = Random.Range(0,5);
            while (CheckUpgrades(chosen) || temp.Contains(chosen))
            {
                chosen++;
                while (temp.Contains(chosen))
                {
                    chosen++;
                }
            }
            temp.Push(chosen);
        }
        while (temp.Count > 0)
        {
            int chosen = temp.Pop();
            switch (chosen)
            {
                case 0:
                    upgrades.Add(new DashDamageUp(upgradeSprites[0]));
                    break;
                case 1:
                    upgrades.Add(new ThornDamageUp(upgradeSprites[1]));
                    break;
                case 2:
                    upgrades.Add(new ShieldSizeUp(upgradeSprites[2]));
                    break;
                case 3:
                    upgrades.Add(new BurnEffect(upgradeSprites[3]));
                    break;
                case 4:
                    upgrades.Add(new FreezeEffect(upgradeSprites[4]));
                    break;
                case 5:
                    upgrades.Add(new HealthUp(upgradeSprites[5]));
                    break;
                default:
                    upgrades.Add(new HealthUp(upgradeSprites[5]));
                    break;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            upgradeBox[i].sprite = upgrades[i].sprite;
        }
    }

    private bool CheckUpgrades(int chosen)
    {
        return !(chosen >= selectedUpgrades.Length) && selectedUpgrades[chosen] >= 3;
    }

    public void ApplyUpgrade()
    {
        StartCoroutine("Apply");
    }


    IEnumerator Apply()
    {
        AudioManager.Instance.Play("mousedown");
        upgrades[curSelected].ApplyUpgrade();
        anim.SetTrigger("close");
        yield return new WaitForSecondsRealtime(0.5f);
        levelUI.SetActive(false);
        Time.timeScale = 1;
    }
}
