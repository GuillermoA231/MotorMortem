using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] obstacles;
    public Transform obstacleSpawnPoint;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private float maxSpawnDelay = 3f;

    [Header("Player Detection")]
    [SerializeField] private float detectionRange = 5f;

    private bool canSpawn = true;
    private GameObject player;

    private void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("No GameObject with tag 'Player' found!");
        }
    }

    private void Update()
    {
        if (canSpawn && !IsPlayerNearby())
        {
            canSpawn = false;
            StartCoroutine(SpawnObstacle());
        }
    }

    private bool IsPlayerNearby()
    {
        if (player == null)
            return false;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        return distance < detectionRange;
    }

    public IEnumerator SpawnObstacle()
    {
        float randomDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(randomDelay);

        if (!IsPlayerNearby())
        {
            GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Length)];
            Instantiate(randomObstacle, obstacleSpawnPoint.transform.position, Quaternion.identity);
        }

        canSpawn = true;
    }

    // Visualize the detection range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}