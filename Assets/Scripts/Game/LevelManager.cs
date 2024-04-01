using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  [SerializeField]
  SceneTransition transition;

  public static LevelManager Instance;
  public bool movementsEnabled = false;


  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void LoadScene(int sceneIndex)
  {
    StartCoroutine(LoadSceneAsync(sceneIndex));
  }

  private IEnumerator LoadSceneAsync(int sceneIndex)
  {
    movementsEnabled = false;

    AsyncOperation scene = SceneManager.LoadSceneAsync(sceneIndex);
    scene.allowSceneActivation = false;

    yield return transition.AnimateTransitionIn();

    scene.allowSceneActivation = true;

    yield return transition.AnimateTransitionOut();
    movementsEnabled = true;
  }
}