using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] coinPrefabs;
    public GameObject[] powerupPrefabs;

    public Transform[] spawnPoints; 

    [Range(0f, 1f)] public float obstacleChance = 0.35f;
    [Range(0f, 1f)] public float coinChance = 0.5f;
    [Range(0f, 1f)] public float powerupChance = 0.02f;

    void Start()
    {
        foreach (Transform point in spawnPoints)
        {
            bool hasSpawn=false;
            float roll = Random.Range(0, 100);

            if (roll < powerupChance * 100 && powerupPrefabs.Length > 0)
            {
                Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], point.position, Quaternion.identity, transform);
                hasSpawn = true;
                //Debug.Log("power");
            }
            else if (roll < (powerupChance + coinChance) * 100 && coinPrefabs.Length > 0 && !hasSpawn)
            {
                Instantiate(coinPrefabs[Random.Range(0, coinPrefabs.Length)], point.position, Quaternion.identity, transform);
                hasSpawn = true;
                //Debug.Log("coin");
            }
            else if (roll < (powerupChance + coinChance + obstacleChance) * 100 && obstaclePrefabs.Length > 0 && !hasSpawn)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], point.position, Quaternion.identity, transform);
                hasSpawn = true;
                //Debug.Log("obstacle");
            }
           // Debug.Log($"Spawning at {point.position}");
        }
    }

}
