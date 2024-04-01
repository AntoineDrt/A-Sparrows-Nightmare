using UnityEngine;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            LevelManager.Instance.LoadScene(1);
        }
    }
}
