using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject Floor;
    [SerializeField] GameObject Fire;
    [SerializeField] GameObject Bush;
    [SerializeField] GameObject Rock;
    [SerializeField] GameObject Tree;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Clone;

    public UnityEvent MapGenerated;

    public TextAsset Blueprint;
    public static MapManager Instance;
    public bool movementsEnabled = false;

    public readonly Dictionary<Vector2Int, GameObject> FloorMap = new();
    public readonly Dictionary<Vector2Int, GameObject> ObjectsMap = new();
    public readonly Dictionary<Vector2Int, GameObject> EntitiesMap = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            MapGenerated ??= new UnityEvent();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMapPosition(Vector2Int oldPosition, Vector2Int newPosition, GameObject entity)
    {
        EntitiesMap.Add(newPosition, entity);
        EntitiesMap.Remove(oldPosition);
    }

    public void LoadMapBlueprint(int index)
    {
        Blueprint = Resources.Load<TextAsset>($"maps/Lv{index}");
    }

    public void GenerateMap(int index)
    {
        CleanUp();
        LoadMapBlueprint(index);

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
                var entity = CharToEntity(c);
                SpawnObject(entity, x, y);
            }

            x++;
        }

        MapGenerated.Invoke();
    }

    public bool IsPositionOccupied(Vector2Int targetPosition)
    {
        return ObjectsMap.ContainsKey(targetPosition);
    }

    public GameObject GetObjectAtPosition(Vector2Int targetPosition)
    {
        try
        {
            return ObjectsMap[targetPosition];
        }
        catch
        {
            return null;
        }
    }

    public GameObject GetEntityAtPosition(Vector2Int targetPosition)
    {
        try
        {
            return EntitiesMap[targetPosition];
        }
        catch
        {
            return null;
        }
    }

    public bool IsInsideMap(Vector2Int targetPosition)
    {
        return FloorMap.ContainsKey(targetPosition);
    }

    private void CleanUp()
    {
        var childCount = transform.childCount;

        for (var i = 0; i < childCount - 1; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private GameObject CharToEntity(char c)
    {
        return c switch
        {
            '6' => Player,
            '9' => Clone,
            'x' => Fire,
            '#' => Bush,
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

    private void SpawnObject(GameObject prefab, int x, int y)
    {
        var instance = Instantiate(
            prefab,
            new Vector3(x, 0.2f, y),
            Quaternion.identity,
            transform
        );

        instance.transform.SetParent(transform);

        if (prefab.name == "Player" || prefab.name == "Clone")
        {
            EntitiesMap.Add(new Vector2Int(x, y), instance);
        }
        else
        {
            if (prefab.name == "Tree")
            {
                instance.transform.localScale = new Vector3(
                    UnityEngine.Random.Range(0.7f, 1.3f),
                    UnityEngine.Random.Range(0.7f, 1.3f),
                    UnityEngine.Random.Range(0.7f, 1.3f)
                );
            }
            ObjectsMap.Add(new Vector2Int(x, y), instance);
        }
    }
}
