using UnityEngine;

public class CloneMovement : PlayerMovement
{
  private Vector3 targetPosition;

  public override void Start()
  {
    base.Start();
    moveInDirection.AddListener(MirrorDirection);
  }

  void Update()
  {
    MoveTo(targetPosition);
  }

  private void MirrorDirection(Vector2Int direction)
  {
    targetPosition = GetTargetPosition(-direction);
    isMoving = CanMoveTo(targetPosition, true);
  }
}