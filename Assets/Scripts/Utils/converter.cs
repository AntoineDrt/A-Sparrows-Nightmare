
using UnityEngine;

public static class Converter
{
  public static Vector2Int To2D(Vector3 position) {
    return new Vector2Int((int)position.x, (int)position.z);
  }
}