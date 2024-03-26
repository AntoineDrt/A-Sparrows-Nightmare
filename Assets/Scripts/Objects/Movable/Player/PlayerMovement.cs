
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : Movable
{
  private Vector2Int currentDirection = Vector2Int.zero;
  private Vector3 targetPosition;

  public override void Start()
  {
    base.Start();
    hasMoved ??= new UnityEvent<Movable>();
    InputsInitializer.InitMoveAction(OnMovePerformed);
    oldPosition = Converter.To2D(transform.position);
  }

  void Update()
  {
    MoveTo(targetPosition);
  }

  public void OnMovePerformed(InputAction.CallbackContext context)
  {
    if (!isMoving)
    {
      Vector2 input = context.ReadValue<Vector2>();

      if (input.x > 0)
      {
        currentDirection = Vector2Int.right;
      }
      else if (input.x < 0)
      {
        currentDirection = Vector2Int.left;
      }
      else if (input.y > 0)
      {
        currentDirection = Vector2Int.up;
      }
      else if (input.y < 0)
      {
        currentDirection = Vector2Int.down;
      }

      targetPosition = transform.position + new Vector3(currentDirection.x, 0f, currentDirection.y);
      isMoving = CanMoveTo(targetPosition);
    }
  }
}