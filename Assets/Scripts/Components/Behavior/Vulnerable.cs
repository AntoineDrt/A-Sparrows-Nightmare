
using UnityEngine;
using UnityEngine.Events;

public class Vulnerable : MonoBehaviour
{
  public UnityEvent GotHurt;
  [SerializeField] Animator animator;

  public void GetHurt()
  {
    animator.SetBool("isDying", true);
    GotHurt.Invoke();
  }
}