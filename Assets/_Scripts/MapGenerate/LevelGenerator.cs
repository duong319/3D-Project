using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;
    public GameObject specialTilePrefab;
    public GameObject enterPortal;
    public float tileLength;
    public int tilesOnScreen;

    public float spawnZ;
    public float spawnYOffset;
    public float specialOffset;
    private float offset;
    private float realtileLength;
    private float ratio;
    private List<GameObject> activeTiles = new List<GameObject>();

    private bool spawnSpecialNext = false;
    private bool isInSpecialMap = false;

    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();

    void Start()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float baseAspect = 9f / 16f;
        ratio = currentAspect / baseAspect;
        offset = specialOffset * ratio;
    

        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
                SpawnTile();
        }

        //foreach (var prefab in tilePrefabs)
        //    CreatePool(prefab, 6);

        //CreatePool(specialTilePrefab, 2);


        //for (int i = 0; i < tilesOnScreen; i++)
        //{
        //    if (i < 2)
        //        SpawnTile(0);
        //    else
        //        SpawnTile();
        //}
    }

    void Update()
    {
        if (player.position.z - 1500f > spawnZ - tilesOnScreen * tileLength && !isInSpecialMap)
        {
            SpawnTile();
            DeleteOldestTile();
        }

        //if (!isInSpecialMap)
        //{
        //    if (player.position.z - 1500f > spawnZ - tilesOnScreen * realtileLength)
        //    {
        //        SpawnTile();
        //        DeleteOldestTile();
        //    }
        //}
    }

    void CreatePool(GameObject prefab, int size)
    {
        if (pools.ContainsKey(prefab)) return;

        Queue<GameObject> q = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            q.Enqueue(obj);
        }

        pools.Add(prefab, q);
    }

    GameObject GetFromPool(GameObject prefab)
    {
        Queue<GameObject> q = pools[prefab];

        if (q.Count == 0)
        {

            GameObject extra = Instantiate(prefab);
            extra.SetActive(false);
            q.Enqueue(extra);
        }

        GameObject obj = q.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    void ReturnToPool(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        pools[prefab].Enqueue(obj);
    }

    GameObject GetPrefabFromInstance(GameObject instance)
    {
        foreach (var pair in pools)
        {

            if (instance.name.Contains(pair.Key.name))
                return pair.Key;
        }
        return null;
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

        //GameObject prefab = (prefabIndex == -1)
        //  ? tilePrefabs[Random.Range(0, tilePrefabs.Length)]
        //  : tilePrefabs[prefabIndex];

        //Vector3 spawnPos = new Vector3(0, spawnYOffset, spawnZ);

        //GameObject tile = GetFromPool(prefab);
        //tile.transform.position = spawnPos;
        //tile.transform.rotation = Quaternion.identity;

        //activeTiles.Add(tile);
        //spawnZ += realtileLength;
    }

    void SpawnSpecialTile()
    {
        foreach (GameObject t in activeTiles)
        {
            Destroy(t);
        }
        activeTiles.Clear();
        spawnZ = Mathf.Floor(player.position.z / tileLength) * tileLength + offset;
        Vector3 spawnPosition = new Vector3(0, spawnYOffset, spawnZ);
        GameObject tile = Instantiate(specialTilePrefab, spawnPosition, Quaternion.identity);
        activeTiles.Add(tile);
        isInSpecialMap = true;

        //foreach (var t in activeTiles)
        //{
        //    GameObject prefab = GetPrefabFromInstance(t);
        //    ReturnToPool(prefab, t);
        //}
        //activeTiles.Clear();


        //spawnZ = Mathf.Floor(player.position.z / realtileLength) * realtileLength + offset;

        //Vector3 pos = new Vector3(0, spawnYOffset, spawnZ);

        //GameObject special = GetFromPool(specialTilePrefab);
        //special.transform.position = pos;
        //special.transform.rotation = Quaternion.identity;

        //activeTiles.Add(special);
        //isInSpecialMap = true;
    }

    void DeleteOldestTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

        //GameObject old = activeTiles[0];
        //activeTiles.RemoveAt(0);

        //GameObject prefab = GetPrefabFromInstance(old);
        //ReturnToPool(prefab, old);
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

        //if (isInSpecialMap)
        //{
        //    StartCoroutine(ActivateEnterPortal());


        //    foreach (var t in activeTiles)
        //    {
        //        ReturnToPool(specialTilePrefab, t);
        //    }
        //    activeTiles.Clear();


        //    spawnZ = Mathf.Floor(player.position.z / realtileLength) * realtileLength;


        //    for (int i = 0; i < tilesOnScreen; i++)
        //    {
        //        if (i < 2)
        //            SpawnTile(0);
        //        else
        //            SpawnTile();
        //    }

        //    isInSpecialMap = false;
        //}
    }

    IEnumerator ActivateEnterPortal()
    {
        enterPortal.gameObject.SetActive(true);
        PlayerController.Instance.forwardSpeed *= 0.9f;
        yield return new WaitForSeconds(1f);
        enterPortal.gameObject.SetActive(false);
    }

}
