using System.Collections;
using UnityEngine;

public class Movable : Object
{
  public Vector2Int oldPosition;
  public Vector2Int currentPosition;
  public float moveSpeed = 10f;
  public bool isMoving = false;
  public Vector2Int currentDirection = Vector2Int.zero;
  public Vector3 targetPosition;

  private bool hasAttacked = false;

  public virtual void Start()
  {
    oldPosition = Converter.To2D(transform.position);
  }

  void Update()
  {
    MoveTo(targetPosition);
  }

  // If the game has ended, no one can move anymore
  public bool IsMovingDisabled()
  {
    return !LevelManager.Instance.movementsEnabled;
  }

  public bool IsInsideMap(Vector2Int targetPosition)
  {
    return mapManager.FloorMap.ContainsKey(targetPosition);
  }

  public bool IsPositionOccupied(Vector2Int targetPosition)
  {
    return mapManager.ObjectsMap.ContainsKey(targetPosition);
  }

  public void MoveTo(Vector3 targetPosition)
  {
    if (isMoving)
    {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

      if (transform.position == targetPosition)
      {
        currentPosition = Converter.To2D(transform.position);
        mapManager.UpdateMapPosition(oldPosition, currentPosition, this);
        oldPosition = currentPosition;
        isMoving = false;
        StartCoroutine(AttackAfterDelay(0.1f));
      }
    }
  }

  private IEnumerator AttackAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    CloneTryAttack(currentPosition);
  }

  private void CloneTryAttack(Vector2Int currentPosition)
  {

    var clone = GameObject.Find("Clone(Clone)");

    var cloneAttack = clone.GetComponent<CloneAttack>();

    if (cloneAttack.CanAttack(currentPosition) && !hasAttacked)
    {
      LevelManager.Instance.GetComponent<EndGame>().onLose();
      hasAttacked = true;
    }
  }

  public Vector3 GetTargetPosition(Vector2Int direction)
  {
    LookInDirection(direction);
    return transform.position + new Vector3(direction.x, 0f, direction.y);
  }

  private void LookInDirection(Vector2Int direction)
  {
    var direction3d = Converter.To3D(direction);

    if (direction3d != Vector3.zero)
    {
      transform.rotation = Quaternion.LookRotation(direction3d);
    }
  }
}