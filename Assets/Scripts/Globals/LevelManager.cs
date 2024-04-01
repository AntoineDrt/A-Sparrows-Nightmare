using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  [SerializeField]
  SceneTransition transition;

  public static LevelManager Instance;
  public bool movementsEnabled = false;
  public bool sceneLoadingInProgress = false;

  private int currentLevel = 0;

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

  public void StartGame()
  {
    LoadScene(1);
    LoadLevel(0);
  }

  public void LoadLevel(int levelIndex)
  {
    MapManager.Instance.GenerateMap(levelIndex);
    currentLevel = levelIndex;
  }

  public void LoadScene(int sceneIndex)
  {
    if (sceneLoadingInProgress) return;

    StartCoroutine(LoadSceneAsync(sceneIndex));
  }

  public IEnumerator LoadSceneAsync(int sceneIndex)
  {
    sceneLoadingInProgress = true;
    movementsEnabled = false;

    AsyncOperation scene = SceneManager.LoadSceneAsync(sceneIndex);
    scene.allowSceneActivation = false;

    yield return transition.AnimateTransitionIn();

    scene.allowSceneActivation = true;

    yield return transition.AnimateTransitionOut();
    sceneLoadingInProgress = false;
    movementsEnabled = true;
  }

  public void OnWin()
  {
    movementsEnabled = false;

    if (SceneManager.GetActiveScene().buildIndex == 5)
    {
      StartCoroutine(LoadMainMenu());
    }

    StartCoroutine(LoadNextLevel());
  }

  public void OnLose()
  {
    movementsEnabled = false;
    StartCoroutine(ReloadLevel());
  }

  IEnumerator LoadNextLevel()
  {
    yield return new WaitForSeconds(1f);
    LoadLevel(currentLevel + 1);
  }

  IEnumerator LoadMainMenu()
  {
    yield return new WaitForSeconds(0.5f);
    LoadScene(0);
  }

  IEnumerator ReloadLevel()
  {
    yield return new WaitForSeconds(0.5f);
    LoadLevel(currentLevel);
  }
}