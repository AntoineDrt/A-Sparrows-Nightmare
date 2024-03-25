using System;
using UnityEngine;

[ExecuteAlways]
public class MapManager : MonoBehaviour
{
    [SerializeField] TextAsset Blueprint;
    [SerializeField] GameObject Floor;
    [SerializeField] GameObject Bomb;
    [SerializeField] GameObject Vent;
    [SerializeField] GameObject Rock;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Clone;

    void Awake()
    {
        CleanUp();

        var x = 0;
        var y = 0;

        foreach (char c in Blueprint.text)
        {
            if (c == '\n')
            {
                x = 0;
                y--;
                continue;
            }

            SpawnFloor(x, y);

            if (c != '.') {
                GameObject entity = CharToEntity(c);
                SpawnEntity(entity, x, y);
            }

            x++;
        }
    }

    private void CleanUp() {
        var childCount = transform.childCount;

        for (var i = 0; i < childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private GameObject CharToEntity(char c)
    {
        switch (c)
        {
            case '6':
                return Player;
            case '9':
                return Clone;
            case 'x':
                return Bomb;
            case '#':
                return Vent;
            case 'o':
                return Rock;
            default:
                throw new Exception($"Could not handle Blueprint character {c}");
        }
    }

    private void SpawnFloor(Int32 x, Int32 y)
    {
        var instance = Instantiate(
            Floor,
            new Vector3(x, 0f, y),
            Quaternion.identity
        );
        instance.transform.SetParent(transform);
    }

    private void SpawnFloor(Int32 x, Int32 y, GameObject prefab)
    {
        var instance = Instantiate(
            prefab,
            new Vector3(x, 0f, y),
            Quaternion.identity,
            transform
        );
        instance.transform.SetParent(transform);

    }

    private void SpawnEntity(GameObject prefab, Int32 x, Int32 y)
    {
        var instance = Instantiate(
            prefab,
            new Vector3(x, 1, y),
            Quaternion.identity,
            transform
        );
        instance.transform.SetParent(transform);
    }
}
