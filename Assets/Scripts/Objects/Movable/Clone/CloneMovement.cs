

using UnityEngine;

public class CloneMovement : PlayerMovement
{
  private Vector3 targetPosition;

  public override void Start()
  {
    moveInDirection.AddListener(MirrorDirection);
  }

  void Update()
  {
    base.Start();
    MoveTo(targetPosition);
  }

  private void MirrorDirection(Vector2Int direction)
  {
    var mirrorDirection = direction;

    if (direction == Vector2Int.down)
    {
      mirrorDirection = Vector2Int.up;
    }
    if (direction == Vector2Int.up)
    {
      mirrorDirection = Vector2Int.down;
    }

    targetPosition = GetTargetPosition(mirrorDirection);
    isMoving = CanMoveTo(targetPosition);
  }
}