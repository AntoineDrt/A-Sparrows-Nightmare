using System;
using System.Collections;
using UnityEngine;

public class Movable : Object
{
  public Vector2Int oldPosition;
  public Vector2Int currentPosition;
  public float moveSpeed = 10f;
  public bool isMoving = false;

  private bool hasAttacked = false;

  public bool CanMoveTo(Vector3 targetPosition, Boolean isClone)
  {
    var targetPosition2D = Converter.To2D(targetPosition);

    // If the game has ended, no one can move anymore
    if(GameObject.Find("GameManager").GetComponent<EndGame>().gameEnded){
      return false;
    }

    if (mapManager.FloorMap.ContainsKey(targetPosition2D))
    {
          // If the clone is the one moving and the target contains a vent obstacle, return true
    if (isClone && mapManager.ObjectsMap.ContainsKey(targetPosition2D))
    {
      if (mapManager.ObjectsMap[targetPosition2D].CompareTag("Vent"))
      {
        return true;
      }
    }  

      if (mapManager.ObjectsMap.ContainsKey(targetPosition2D))
      {
        // Depending on who steps on the bomb, the game is won or lost
        if (mapManager.ObjectsMap[targetPosition2D].CompareTag("Bomb"))
        {
          if(!isClone){
            GameObject.Find("GameManager").GetComponent<EndGame>().onLose();
          } else {
            GameObject.Find("GameManager").GetComponent<EndGame>().onWin();
          }
          return true;
        }
        return false;
      }
      return true;
    }

    return false;
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
        StartCoroutine(AttackAfterDelay(0.3f));
      }
    }
  }

    private IEnumerator AttackAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    CloneTryAttack(currentPosition);
  }

    private void CloneTryAttack(Vector2Int currentPosition){

    var clone = GameObject.Find("Clone(Clone)");

    var cloneAttack = clone.GetComponent<CloneAttack>();

    if (cloneAttack.CanAttack(currentPosition) && !hasAttacked) 
    {
      GameObject.Find("GameManager").GetComponent<EndGame>().onLose();
      hasAttacked = true;
    }
  }

  public Vector3 GetTargetPosition(Vector2Int direction)
  {
    return transform.position + new Vector3(direction.x, 0f, direction.y);
  }
}