
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : Movable
{
  public UnityEvent<Vector2Int> moveInDirection;
  
  private Vector2Int currentDirection = Vector2Int.zero;
  private Vector3 targetPosition;

  public virtual void Start()
  {
    moveInDirection ??= new UnityEvent<Vector2Int>();
    oldPosition = Converter.To2D(transform.position);
    InputsInitializer.InitMoveAction(OnMovePerformed);
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

      targetPosition = GetTargetPosition(currentDirection);
      isMoving = CanMoveTo(targetPosition);
      moveInDirection.Invoke(currentDirection);
    }
  }
}