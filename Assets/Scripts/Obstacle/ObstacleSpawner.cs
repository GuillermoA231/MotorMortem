using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] obstacles;

    public Transform obstacleSpawnPoint;

    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 3f;

    private bool canSpawn = true;

    private void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnObstacle());
        }
    }
    public IEnumerator SpawnObstacle()
    {
        float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(randomDelay);

        GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Length)];

        Instantiate(randomObstacle, obstacleSpawnPoint.transform.position, Quaternion.identity);

        canSpawn = true;
    }
}
