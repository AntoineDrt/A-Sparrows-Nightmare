
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vulnerable : MonoBehaviour
{
  public UnityEvent GotHurt;

  [SerializeField] Animator animator;
  [SerializeField] List<GameObject> vulnerabilityList;

  public bool IsVulnerableTo(GameObject entity)
  {
    return vulnerabilityList.Contains(entity);
  }

  public void GetHurt()
  {
    animator.SetBool("isDying", true);
    GotHurt.Invoke();
  }
}