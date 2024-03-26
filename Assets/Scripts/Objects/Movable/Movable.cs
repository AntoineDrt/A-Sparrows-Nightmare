
using UnityEngine;
using UnityEngine.Events;

public class Movable : MonoBehaviour {

  public UnityEvent<Movable> hasMoved;
  public MapManager mapManager;
  public Vector2Int oldPosition;
  public Vector2Int currentPosition;
  public float moveSpeed = 5f;
  public bool isMoving = false;
  
  public virtual void Start() {
    mapManager = FindObjectOfType<MapManager>();
  }

  public bool CanMoveTo(Vector3 targetPosition)
  {
    var targetPosition2D = Converter.To2D(targetPosition);

    if (mapManager.FloorMap.ContainsKey(targetPosition2D))
    {
      if (mapManager.ObjectsMap.ContainsKey(targetPosition2D))
      {
        return false;
      }
      return true;
    }

    return false;
  }

  public void MoveTo(Vector3 targetPosition) {
    if (isMoving)
    {
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

      if (transform.position == targetPosition)
      {
        currentPosition = Converter.To2D(transform.position);
        mapManager.UpdateMapPosition(oldPosition, currentPosition, gameObject);
        oldPosition = currentPosition;
        hasMoved.Invoke(this);
        isMoving = false;
      }
    }
  }
}