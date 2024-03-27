using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{

    [SerializeField] private GameObject whiteFadeIn;
    [SerializeField] private GameObject whiteFadeOut;
    public Boolean gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {

        whiteFadeOut.SetActive(true);
    }

    public void onWin()
    {
        gameEnded = true;
        whiteFadeIn.SetActive(true);
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onLose()
    {
        gameEnded = true;
        whiteFadeIn.SetActive(true);
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
