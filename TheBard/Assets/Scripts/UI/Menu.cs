using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public AudioClip jingleIntro;
    public AudioClip menuMusic;

    private void Start()
    {
        AudioManager.Instance.PlaySound(jingleIntro);
    }

    private void Update()
    {
        if(!AudioManager.Instance.isPlaying())
        {
            AudioManager.Instance.setAudioClip(menuMusic);
            AudioManager.Instance.setLoop(true);
            AudioManager.Instance.Play();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
