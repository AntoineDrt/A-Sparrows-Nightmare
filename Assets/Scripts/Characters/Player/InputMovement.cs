
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMovement : MonoBehaviour
{
  public float moveSpeed = 5f;

  private Vector2Int currentDirection = Vector2Int.zero;
  private Vector3 targetPosition;
  private bool isMoving = false;

  void Start()
  {
    InputsInitializer.InitMoveAction(OnMovePerformed);
  }

  void Update()
  {
    if (isMoving)
    {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

      if (transform.position == targetPosition)
      {
        isMoving = false;
      }
    }
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
      isMoving = true;
    }
  }
}