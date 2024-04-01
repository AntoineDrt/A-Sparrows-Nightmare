using System.Collections.Generic;
using UnityEngine;

public class CloneAttack : MonoBehaviour
{
    // private Animator animator;

    // void Start()
    // {
    //     mapManager = this.mapManager ?? FindObjectOfType<MapManager>();
    //     animator = gameObject.GetComponentInChildren<Animator>();
    // }

    // public bool CanAttack(Vector2Int currentPosition)
    // // {
    //     var adjacentTiles = new List<Vector2Int>
    //     {
    //         new Vector2Int(1, 0),
    //         new Vector2Int(-1, 0),
    //         new Vector2Int(0, 1),
    //         new Vector2Int(0, -1),
    //         new Vector2Int(1, 1),
    //         new Vector2Int(-1, -1),
    //         new Vector2Int(1, -1),
    //         new Vector2Int(-1, 1)
    //     };

    //     foreach (var tile in adjacentTiles)
    //     {
    //         var targetPosition2D = currentPosition + new Vector2Int(tile.x, tile.y);

    //         if (mapManager.EntitiesMap.ContainsKey(targetPosition2D))
    //         {
    //             if (mapManager.EntitiesMap[targetPosition2D].CompareTag("Player"))
    //             {
    //                 animator.SetBool("isAttacking", true);

    //                 var player = GameObject.FindGameObjectWithTag("Player");
    //                 transform.LookAt(player.transform);

    //                 return true;
    //             }
    //         }
    //     }

    //     return false;
        
    // }
}
