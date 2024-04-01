using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void onWin()
    {
        LevelManager.Instance.movementsEnabled = false;

        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            StartCoroutine(LoadMainMenu());
        }
        else
        {
            StartCoroutine(LoadNextScene());
        }
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.5f);
        LevelManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        LevelManager.Instance.LoadScene(0);
    }

    public void onLose()
    {
        LevelManager.Instance.movementsEnabled = false;

        var player = GameObject.FindGameObjectWithTag("Player");
        var animator = player.GetComponentInChildren<Animator>();
        animator.SetBool("isDying", true);

        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(0.5f);
        LevelManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
