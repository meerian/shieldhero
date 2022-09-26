using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnim;

    public Image muteButton;
    public Sprite muted;
    public Sprite notMuted;

    public void StartGame()
    {
        AudioManager.Instance.Play("mousedown");
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int levelindex)
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(levelindex);
    }

    public void ToggleMute()
    {
        AudioManager.Instance.ToggleMute();
        if (AudioManager.Instance.isMuted)
        {
            muteButton.sprite = muted;
        }
        else
        {
            muteButton.sprite = notMuted;
        }
    }
}
