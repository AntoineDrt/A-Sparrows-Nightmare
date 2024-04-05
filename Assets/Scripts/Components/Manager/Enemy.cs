
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
  [SerializeField] Vulnerable vulnerable;

  private void Start() 
  {
    vulnerable.GotHurt.AddListener(LevelManager.Instance.OnWin);
  }
}