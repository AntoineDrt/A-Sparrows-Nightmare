
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
public class AttackProximity : MonoBehaviour
{

  [SerializeField] Movable movable;
  [SerializeField] Animator animator;

  private List<Vector2Int> adjacentDirections;
  private bool hasAttacked = false;

  private void Start()
  {
    TurnManager.Instance.ActionPhase.AddListener(TryToAttack);
    adjacentDirections = GetAdjacentDirections();
  }

  private void OnDestroy()
  {
    TurnManager.Instance.ActionPhase.RemoveListener(TryToAttack);
  }

  private void TryToAttack()
  {
    var target = FindTarget();

    if (target)
    {
      Attack(target);
    }

    TurnManager.Instance.ActionPhase.Done();
  }

  private GameObject FindTarget()
  {
    if (hasAttacked) return null;

    foreach (var direction in adjacentDirections)
    {
      var adjacentPosition = movable.currentPosition + direction;
      var adjacentObject = MapManager.Instance.GetEntityAtPosition(adjacentPosition);

      if (adjacentObject && adjacentObject.CompareTag("Player"))
      {
        movable.LookInDirection(direction);
        return adjacentObject;
      }
    }

    return null;
  }

  private void Attack(GameObject target)
  {
    var vulnerable = target.GetComponent<Vulnerable>();
    hasAttacked = true;
    
    animator.SetBool("isAttacking", true);
    vulnerable.GetHurt();
  }

  private List<Vector2Int> GetAdjacentDirections()
  {
    var adjacentTiles = new List<Vector2Int>();

    for (int x = -1; x <= 1; x++)
    {
      for (int y = -1; y <= 1; y++)
      {
        if (x != 0 || y != 0)
        {
          adjacentTiles.Add(new Vector2Int(x, y));
        }
      }
    }

    return adjacentTiles;
  }
}