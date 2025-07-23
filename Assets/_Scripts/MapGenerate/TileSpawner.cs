using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] coinPrefabs;
    public GameObject[] powerupPrefabs;

    public Transform[] spawnPoints; 

    [Range(0f, 1f)] public float obstacleChance = 0.35f;
    [Range(0f, 1f)] public float coinChance = 0.5f;
    [Range(0f, 1f)] public float powerupChance = 0.1f;

    void Start()
    {
        foreach (Transform point in spawnPoints)
        {
            float roll = Random.value;

            if (roll < powerupChance && powerupPrefabs.Length > 0)
            {
                Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], point.position, Quaternion.identity, transform);
                //Debug.Log("power");
            }
            else if (roll < powerupChance + coinChance && coinPrefabs.Length > 0)
            {
                Instantiate(coinPrefabs[Random.Range(0, coinPrefabs.Length)], point.position, Quaternion.identity, transform);
                //Debug.Log("coin");
            }
            else if (roll < powerupChance + coinChance + obstacleChance && obstaclePrefabs.Length > 0)
            {
                Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], point.position, Quaternion.identity, transform);
                //Debug.Log("obstacle");
            }
        }
    }

}
