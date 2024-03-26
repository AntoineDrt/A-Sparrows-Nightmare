using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAttack : Object
{
    // Start is called before the first frame update
    void Start()
    {
        mapManager = this.mapManager ?? FindObjectOfType<MapManager>();
    }

    public bool canAttack(Vector2Int currentPosition)
    {
        // Check every adjacent tiles, diagonals included. If the player is on any of them, return true.
        var adjacentTiles = new List<Vector2Int>
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1)
        };

        foreach (var tile in adjacentTiles)
        {
            var targetPosition2D = currentPosition + new Vector2Int(tile.x, tile.y);

            if (mapManager.ObjectsMap.ContainsKey(targetPosition2D))
            {
                if (mapManager.ObjectsMap[targetPosition2D].GetType() == typeof(PlayerMovement))
                {
                    return true;
                }
            }
        }

        return false;
        
    }
}
