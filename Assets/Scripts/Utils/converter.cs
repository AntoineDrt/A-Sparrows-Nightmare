
using UnityEngine;

public static class Converter
{
  public static Vector2Int To2D(Vector3 position)
  {
    return new Vector2Int((int)position.x, (int)position.z);
  }

  public static Vector3 To3D(Vector2Int position)
  {
    return new Vector3(position.x, 0f, position.y);
  }
}