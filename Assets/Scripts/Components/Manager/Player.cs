
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
  [SerializeField] Vulnerable vulnerable;

  void Start() 
  {
    vulnerable.GotHurt.AddListener(LevelManager.Instance.OnLose);
  }


}