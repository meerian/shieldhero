using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public GameObject[] selections;
    public Text timeText;
    public int curSelected = 0;

    public GameObject transition;

    // Start is called before the first frame update
    void Start()
    {
        var timespan = TimeSpan.FromSeconds(GameManager.Instance.timeTaken);
        timeText.text = timespan.ToString(@"mm\:ss");
    }

    public void ShowDetail(int selection)
    {
        AudioManager.Instance.Play("mouseover");
        selections[curSelected].SetActive(false);
        selections[selection].SetActive(true);
        curSelected = selection;
    }

    public void ConfirmSelection()
    {
        switch (curSelected)
        {
            case 0:
                StartCoroutine(LoadLevel(1));
                return;
            case 1:
                StartCoroutine(LoadLevel(0));
                return;
            default:
                return;
        }
    }

    IEnumerator LoadLevel(int levelindex)
    {
        transition.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1f;
        SceneManager.LoadScene(levelindex);
    }

}
