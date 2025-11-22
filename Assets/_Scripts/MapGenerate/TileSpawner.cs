using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] coinPrefabs;
    public GameObject[] powerupPrefabs;

    public Transform[] spawnPoints;

    [Range(0f, 1f)] public float obstacleChance;
    [Range(0f, 1f)] public float coinChance;
    [Range(0f, 1f)] public float powerupChance;

    void Start()
    {
        foreach (Transform point in spawnPoints)
        {
            bool hasSpawn = false;
            float roll = Random.Range(0, 100);

            if (roll < powerupChance * 100 && powerupPrefabs.Length > 0)
            {
                Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], point.position, Quaternion.identity, transform);
                hasSpawn = true;
            }
            else if (roll < (powerupChance + coinChance) * 100 && coinPrefabs.Length > 0 && !hasSpawn)
            {
                Instantiate(coinPrefabs[Random.Range(0, coinPrefabs.Length)], point.position, Quaternion.identity, transform);
                hasSpawn = true;
            }
            else if (roll < (powerupChance + coinChance + obstacleChance) * 100 && obstaclePrefabs.Length > 0 && !hasSpawn)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], point.position, Quaternion.identity, transform);
                hasSpawn = true;
            }
        }
    }
}
