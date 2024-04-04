
using System;
using UnityEngine;

public class InteractOnContact : MonoBehaviour
{
  [SerializeField] Action interaction;

  // void FixedUpdate()
  // {
  //   if ()
  // }

  public void ContactWith(Vulnerable entity)
  {
    entity.GotHurt.Invoke();
  }
}