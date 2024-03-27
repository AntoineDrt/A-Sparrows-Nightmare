using UnityEngine;
using UnityEngine.InputSystem;

public class CloneMovement : Movable
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
        currentDirection = Vector2Int.left;
      }
      else if (input.x < 0)
      {
        currentDirection = Vector2Int.right;
      }
      else if (input.y > 0)
      {
        currentDirection = Vector2Int.down;
      }
      else if (input.y < 0)
      {
        currentDirection = Vector2Int.up;
      }

      targetPosition = GetTargetPosition(currentDirection);
      isMoving = CanCloneMoveTo(targetPosition);
    }
  }

  public bool CanCloneMoveTo(Vector3 targetPosition)
  {
    var targetPosition2D = Converter.To2D(targetPosition);

    if (IsMovingDisabled() || !IsInsideMap(targetPosition2D))
    {
      return false;
    }

    if (IsPositionOccupied(targetPosition2D))
    {
      if (mapManager.ObjectsMap[targetPosition2D].CompareTag("Vent"))
      {
        return true;
      }

      if (mapManager.ObjectsMap[targetPosition2D].CompareTag("Bomb"))
      {
        GameObject.Find("GameManager").GetComponent<EndGame>().onWin();
        return true;
      }

      return false;
    }

    return true;
  }
}