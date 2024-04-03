
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScaleSpawn : MonoBehaviour
{
  public void Spawn(float finalScale, float time)
  {
    transform.DOScale(finalScale, time);
  }

  public IEnumerator SpawnAsync(float finalScale, float time)
  {
    Spawn(finalScale, time);
    yield return new WaitForSeconds(time);
  }
}