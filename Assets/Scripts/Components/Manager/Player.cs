
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
  [SerializeField] Vulnerable vulnerable;

  private void Start() 
  {
    vulnerable.GotHurt.AddListener(LevelManager.Instance.OnLose);
  }
}