using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private Button PlayButton;

    void Start()
    {
        PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
    }

    public void OnClickPlayButton()
    {
        // Go to the next scene in build;
        SceneManager.LoadScene(1);
    }
}
