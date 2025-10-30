using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;
    public GameObject specialTilePrefab;
    public GameObject enterPortal;
    public float tileLength = 400f;
    public int tilesOnScreen = 5;

    public float spawnZ = 0f;
    public float spawnYOffset = -1.5f;
    public float specialOffset = 500f;
    private List<GameObject> activeTiles = new List<GameObject>();

    private bool spawnSpecialNext = false;
    private bool isInSpecialMap = false;

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
        if (player.position.z - 1500f > spawnZ - tilesOnScreen * tileLength && !isInSpecialMap)
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

    void SpawnSpecialTile()
    {
        foreach (GameObject t in activeTiles)
        {
            Destroy(t);
        }
        activeTiles.Clear();
        spawnZ = Mathf.Floor(player.position.z / tileLength) * tileLength + specialOffset;
        Vector3 spawnPosition = new Vector3(0, spawnYOffset, spawnZ);
        GameObject tile = Instantiate(specialTilePrefab, spawnPosition, Quaternion.identity);
        activeTiles.Add(tile);
        isInSpecialMap = true;
    }

    void DeleteOldestTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public void EnterPortal()
    {
        if (!isInSpecialMap)
        {
            StartCoroutine(ActivateEnterPortal());
            spawnSpecialNext = true;
            SpawnSpecialTile();
        }
    }

    public void ExitPortal()
    {
        if (isInSpecialMap)
        {
            StartCoroutine(ActivateEnterPortal());
            foreach (GameObject t in activeTiles)
            {
                Destroy(t);
            }
            activeTiles.Clear();
       
            spawnZ = Mathf.Floor(player.position.z / tileLength) * tileLength;

            for (int i = 0; i < tilesOnScreen; i++)
            {
                if (i < 2)
                    SpawnTile(0);
                else
                    SpawnTile();
            }
            isInSpecialMap = false;
        }
    }

    IEnumerator ActivateEnterPortal()
    {
        enterPortal.gameObject.SetActive(true);
        PlayerController.Instance.forwardSpeed *= 0.9f;
        yield return new WaitForSeconds(1f);
        enterPortal.gameObject.SetActive(false);
    }

}
