using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;     
    public float tileLength = 400f;        
    public int tilesOnScreen = 5;

    public float spawnZ = 0f;
    public float spawnYOffset = -1.5f;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i < 2)
                SpawnTile(0); 
            else
                SpawnTile();  
        }
    }

    void Update()
    {
        if (player.position.z - 1500f > spawnZ - tilesOnScreen * tileLength)
        {
            SpawnTile();
            DeleteOldestTile();
        }
    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject prefab;
        if (prefabIndex == -1)
            prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
        else
            prefab = tilePrefabs[prefabIndex];
        Vector3 spawnPosition = new Vector3(0, spawnYOffset, spawnZ);
        GameObject tile = Instantiate(prefab, spawnPosition, Quaternion.identity);
        activeTiles.Add(tile);
        spawnZ += tileLength;
    }

    void DeleteOldestTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
