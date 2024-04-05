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

  private void Start() 
  {
    MapManager.Instance.MapGenerated.AddListener(OnMapGenerated);
  }

  public void StartGame()
  {
    StartCoroutine(StartGameAsync());
  }

  public IEnumerator StartGameAsync()
  {
    yield return LoadSceneAsync(1);
    StartCoroutine(LoadLevelAsync(0));
  }

  public IEnumerator LoadLevelAsync(int levelIndex)
  {
    yield return transition.AnimateTransitionIn();
    MapManager.Instance.GenerateMap(levelIndex);
    currentLevel = levelIndex;
  }

  public void ReloadLevel()
  {
    StartCoroutine(LoadLevelAsync(currentLevel));
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

    LoadNextLevel();
  }

  public void OnLose()
  {
    StartCoroutine(OnLoseAsync());
  }

  public IEnumerator OnLoseAsync() 
  {
    movementsEnabled = false;
    yield return transition.AnimateTransitionIn();
    ReloadLevel();
  }

  void LoadNextLevel()
  {
    StartCoroutine(LoadLevelAsync(currentLevel + 1));
  }

  IEnumerator LoadMainMenu()
  {
    yield return new WaitForSeconds(0.5f);
    LoadScene(0);
  }

  private void OnMapGenerated()
  {
    StartCoroutine(OnMapGeneratedAsync());
  }
  
  private IEnumerator OnMapGeneratedAsync()
  {
    yield return transition.AnimateTransitionOut();
    yield return MapManager.Instance.AnimateMapSpawn();
    movementsEnabled = true;
  }
}