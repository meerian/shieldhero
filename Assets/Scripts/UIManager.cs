using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Slider expbar;

    public Slider dashbar;

    public Animator dashAnim;

    public Text healthText;

    public GameObject flashHurt;

    public GameObject gameoverPanel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"x{GameManager.Instance.player.GetComponent<PlayerController>().health}";
        expbar.value = GameManager.Instance.exp;
        expbar.maxValue = GameManager.Instance.maxexp;
        dashbar.value = Time.time - GameManager.Instance.player.GetComponent<PlayerController>().prevDash;
    }

    public void GameOver()
    {
        flashHurt.SetActive(false);
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
    }

    public void ShowDashAnim()
    {
        if (dashbar.value >= dashbar.maxValue)
        {
            dashAnim.SetTrigger("isFull");
            AudioManager.Instance.Play("beep");
        }       
    }

    public void FlashHurt()
    {
        StartCoroutine("ShowHurt");
    }

    IEnumerator ShowHurt()
    {
        flashHurt.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        flashHurt.SetActive(false);
    }
}
