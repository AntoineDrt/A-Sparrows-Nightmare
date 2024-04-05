using System;
using System.Collections;
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

    public readonly Dictionary<Vector2Int, GameObject> FloorMap = new();
    public readonly Dictionary<Vector2Int, GameObject> ObjectsMap = new();
    public readonly Dictionary<Vector2Int, GameObject> EntitiesMap = new();

    public static MapManager Instance;
    public UnityEvent MapGenerated;
    public TextAsset Blueprint;

    private GameObject mapContainer;
    private bool loadingInProgress = false;

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
        EntitiesMap.Remove(oldPosition);
        EntitiesMap.Add(newPosition, entity);
    }

    public void LoadMapBlueprint(int index)
    {
        Blueprint = Resources.Load<TextAsset>($"maps/Lv{index}");
    }

    public void GenerateMap(int index)
    {
        if (loadingInProgress) return;

        loadingInProgress = true;

        CleanUp();
        LoadMapBlueprint(index);

        mapContainer = new GameObject("Container");
        mapContainer.transform.SetParent(transform);

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

            InstantiateFloor(x, y);

            if (c != '.')
            {
                var entity = CharToEntity(c);
                InstantiateObject(entity, x, y);
            }

            x++;
        }

        loadingInProgress = false;

        MapGenerated.Invoke();
    }

    public IEnumerator AnimateMapSpawn()
    {
        foreach (var mapObject in ObjectsMap.Values)
            yield return mapObject.GetComponent<ScaleSpawn>().Animate();

        foreach (var entity in EntitiesMap.Values)
            yield return entity.GetComponent<ScaleSpawn>().Animate();
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
        Destroy(mapContainer);

        FloorMap.Clear();
        ObjectsMap.Clear();
        EntitiesMap.Clear();
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

    private void InstantiateFloor(int x, int y)
    {
        var instance = Instantiate(
            Floor,
            new Vector3(x, 0f, y),
            Quaternion.identity
        );

        instance.transform.SetParent(mapContainer.transform);

        FloorMap.Add(new Vector2Int(x, y), instance);
    }

    private void InstantiateObject(GameObject prefab, int x, int y)
    {
        var instance = Instantiate(
            prefab,
            new Vector3(x, 0.2f, y),
            Quaternion.identity
        );

        instance.transform.SetParent(mapContainer.transform);
        instance.SetActive(false);

        if (prefab.name == "Player" || prefab.name == "Clone")
        {
            EntitiesMap.Add(new Vector2Int(x, y), instance);
        }
        else
        {
            ObjectsMap.Add(new Vector2Int(x, y), instance);
        }
    }
}
