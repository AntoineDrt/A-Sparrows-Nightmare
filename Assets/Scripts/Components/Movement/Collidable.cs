
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
  [SerializeField] List<GameObject> collisionList;

  private readonly MapManager mapManager = MapManager.Instance;

  public bool CanMoveTo(Vector2Int targetPosition)
  {
    var obstacle = mapManager.GetObjectAtPosition(targetPosition);
    return obstacle ? !CollidesWith(obstacle) : true;
  }

  private bool CollidesWith(GameObject obstacle)
  {
    foreach (var blockingObstacle in collisionList)
    {
      if (blockingObstacle.CompareTag(obstacle.tag))
      {
        return true;
      }
    }

    return false;
  }
}