using System.Collections;
using TMPro;
using UnityEngine;

public class Movable : MonoBehaviour
{
  [SerializeField] Collidable collidable;

  public Vector2Int oldPosition;
  public Vector2Int currentPosition;
  public Vector2Int currentDirection = Vector2Int.zero;
  public Vector2Int targetPosition;

  private readonly MapManager mapManager = MapManager.Instance;
  private readonly LevelManager levelManager = LevelManager.Instance;
  private readonly float moveSpeed = 10f;
  private bool isMoving = false;

  public virtual void Start()
  {
    oldPosition = Converter.To2D(transform.position);
  }

  void Update()
  {
    if (isMoving)
    {
      MoveTo(targetPosition);
    }
  }

  public bool IsMovingDisabled()
  {
    return !levelManager.movementsEnabled;
  }

  public void TryToMoveTo(Vector2Int position)
  {
    if (IsMovingDisabled() ||
      !mapManager.IsInsideMap(position) ||
      (collidable && !collidable.CanMoveTo(position)))
    {
      return;
    }

    targetPosition = position;
    isMoving = true;
  }

  public void MoveTo(Vector2Int targetPosition)
  {
    Vector3 targetPosition3D = Converter.To3D(targetPosition);

    transform.position = Vector3.MoveTowards(
      transform.position,
      targetPosition3D,
      moveSpeed * Time.deltaTime);

    if (transform.position == targetPosition3D)
    {
      currentPosition = Converter.To2D(transform.position);
      mapManager.UpdateMapPosition(oldPosition, currentPosition, gameObject);
      oldPosition = currentPosition;
      isMoving = false;
    }
  }

  public void LookInDirection(Vector2Int direction)
  {
    var direction3d = Converter.To3D(direction);

    if (direction3d != Vector3.zero)
    {
      transform.rotation = Quaternion.LookRotation(direction3d);
    }
  }

  public Vector3 GetTargetPosition(Vector2Int direction)
  {
    LookInDirection(direction);
    return transform.position + new Vector3(direction.x, 0f, direction.y);
  }
}