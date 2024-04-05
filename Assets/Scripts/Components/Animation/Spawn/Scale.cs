
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScaleSpawn : MonoBehaviour
{
  public IEnumerator Animate()
  {
    gameObject.SetActive(true);
    yield return SpawnAsync(1.5f, 0.07f);
    StartCoroutine(SpawnAsync(1, 0.05f));
  }

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