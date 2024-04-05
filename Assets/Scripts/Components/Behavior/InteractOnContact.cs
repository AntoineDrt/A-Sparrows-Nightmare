
using System;
using UnityEngine;

public class InteractOnContact : MonoBehaviour
{
  [SerializeField] Action interaction;

  private void Start()
  {
    TurnManager.Instance.ActionPhase.AddListener(ContactWith);
  }

  private void OnDestroy()
  {
    TurnManager.Instance.ActionPhase.RemoveListener(ContactWith);
  }

  public void ContactWith()
  {
    var currentPosition = Converter.To2D(gameObject.transform.position);
    var entityInContact = MapManager.Instance.GetEntityAtPosition(currentPosition);

    if (entityInContact != null)
    {
      entityInContact.GetComponent<Vulnerable>().GotHurt.Invoke();
    }

    TurnManager.Instance.ActionPhase.Done();
  }
}