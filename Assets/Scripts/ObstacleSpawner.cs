using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] obstacles;

    public Transform obstacleSpawnPoint;

    public float obstacleSpawnDelay;

    private bool canSpawn = true;

    private void Update()
    {
        if(canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnObstacle());
        }
    }
    public IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(obstacleSpawnDelay);

        GameObject randomObstacle =  obstacles[Random.Range(0,obstacles.Length)];

        Instantiate(randomObstacle,obstacleSpawnPoint.transform.position,Quaternion.identity);

        canSpawn =true;
    }
}
