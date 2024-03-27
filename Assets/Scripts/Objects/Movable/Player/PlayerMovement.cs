
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : Movable
{

  public override void Start()
  {
    base.Start();
    InputsInitializer.InitMoveAction(OnMovePerformed);
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
      isMoving = CanMoveTo(Converter.To2D(targetPosition));
    }
  }

  private bool CanMoveTo(Vector2Int targetPosition)
  {
    if (IsMovingDisabled() || !IsInsideMap(targetPosition))
    {
      return false;
    }

    // There is an object on target position
    if (IsPositionOccupied(targetPosition))
    {
      // Depending on who steps on the bomb, the game is won or lost
      if (mapManager.ObjectsMap[targetPosition].CompareTag("Bomb"))
      {
        GameObject.Find("GameManager").GetComponent<EndGame>().onLose();
        return true;
      }
      return false;
    }
    return true;
  }
}

