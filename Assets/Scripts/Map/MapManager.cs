using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    [SerializeField] TextAsset Blueprint;
    [SerializeField] GameObject Floor;
    [SerializeField] Object Bomb;
    [SerializeField] Object Vent;
    [SerializeField] Object Rock;
    [SerializeField] Object Tree;
    [SerializeField] PlayerMovement Player;
    [SerializeField] CloneMovement Clone;

    public UnityEvent Ready;

    public Dictionary<Vector2Int, GameObject> FloorMap = new();
    public Dictionary<Vector2Int, Object> ObjectsMap = new();

    public Dictionary<Vector2Int, Object> EntitiesMap = new();

    void Start()
    {
        Ready ??= new UnityEvent();
        CleanUp();
        GenerateMap();
        Ready.Invoke();
    }

    public void UpdateMapPosition(Vector2Int oldPosition, Vector2Int newPosition, Object entity)
    {
        EntitiesMap.Add(newPosition, entity);
        EntitiesMap.Remove(oldPosition);
    }

    private void GenerateMap()
    {
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

            if (c != '.')
            {
                Object entity = CharToEntity(c);
                SpawnObject(entity, x, y);
            }

            x++;
        }
    }

    private void CleanUp()
    {
        var childCount = transform.childCount;

        for (var i = 0; i < childCount - 1; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private Object CharToEntity(char c)
    {
        return c switch
        {
            '6' => Player,
            '9' => Clone,
            'x' => Bomb,
            '#' => Vent,
            'o' => Rock,
            'T' => Tree,
            _ => throw new Exception($"Could not handle Blueprint character {c}"),
        };
    }

    private void SpawnFloor(int x, int y)
    {
        var instance = Instantiate(
            Floor,
            new Vector3(x, 0f, y),
            Quaternion.identity
        );

        instance.transform.SetParent(transform);
        FloorMap.Add(new Vector2Int(x, y), instance);
    }

    private void SpawnFloor(int x, int y, GameObject prefab)
    {
        var instance = Instantiate(
            prefab,
            new Vector3(x, 0f, y),
            Quaternion.identity,
            transform
        );

        instance.transform.SetParent(transform);
        FloorMap.Add(new Vector2Int(x, y), instance);
    }

    private void SpawnObject(Object prefab, int x, int y)
    {
        var instance = Instantiate(
            prefab,
            new Vector3(x, 0.2f, y),
            Quaternion.identity,
            transform
        );

        instance.mapManager = this;
        instance.transform.SetParent(transform);
        if (prefab.name == "Player" || prefab.name == "Clone")
        {
            EntitiesMap.Add(new Vector2Int(x, y), instance);
        }
        else
        {
            // if (prefab.name == "Tree")
            // {
            //     instance.transform.localScale = new Vector3(
            //         UnityEngine.Random.Range(0.7f, 1.3f),
            //         UnityEngine.Random.Range(0.7f, 1.3f),
            //         UnityEngine.Random.Range(0.7f, 1.3f)
            //     );
            // }
            ObjectsMap.Add(new Vector2Int(x, y), instance);
        }
    }
}
