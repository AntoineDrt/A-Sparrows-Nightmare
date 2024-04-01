using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey && !LevelManager.Instance.sceneLoadingInProgress)
        {
            LevelManager.Instance.StartGame();
        }
    }
}
