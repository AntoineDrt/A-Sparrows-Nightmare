using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Canvas PauseMenuCanvas;
    private Boolean isPaused = false;

    void Start()
    {
        PauseMenuCanvas = GameObject.Find("PauseMenu").GetComponent<Canvas>();
        PauseMenuCanvas.enabled = false;   
    }

    public void PauseGame(bool currentlyPaused)
    {
        Time.timeScale = currentlyPaused ? 1 : 0;
        PauseMenuCanvas.enabled = currentlyPaused ? false : true;

        isPaused = !isPaused;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(isPaused);
        }   
    }

    public void OnMuteMusic()
    {
        var audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();
        Debug.Log(audioSource);
        audioSource.mute = !audioSource.mute;
    }
}
