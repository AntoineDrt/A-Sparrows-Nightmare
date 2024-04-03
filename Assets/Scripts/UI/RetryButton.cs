
using UnityEngine;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
  void Start()
  {
    var button = GetComponent<Button>();
    button.onClick.AddListener(LevelManager.Instance.ReloadLevel);
  }
}