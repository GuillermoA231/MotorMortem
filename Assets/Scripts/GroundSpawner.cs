using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    [System.Serializable]
    public class GroundTileData
    {
        public GameObject tilePrefab;
        [Range(0f, 1f)]
        public float weight = 1f;
    }

    [SerializeField]
    private List<GroundTileData> groundTiles = new List<GroundTileData>();

    [SerializeField]
    [Range(0f, 1f)]
    private float consecutivePenalty = 0.7f; // Reduces chance of same tile appearing consecutively

    private Vector3 nextSpawnPoint;
    private int lastSpawnedIndex = -1;

    public void SpawnTile()
    {
        if (groundTiles == null || groundTiles.Count == 0)
        {
            Debug.LogWarning("No ground tiles assigned!");
            return;
        }

        int selectedIndex = SelectWeightedRandomTile();
        GameObject selectedTile = groundTiles[selectedIndex].tilePrefab;

        GameObject temp = Instantiate(selectedTile, nextSpawnPoint, Quaternion.identity);
        Transform lastChild = temp.transform.GetChild(temp.transform.childCount - 1);
        nextSpawnPoint = lastChild.position;

        lastSpawnedIndex = selectedIndex;
    }

    private int SelectWeightedRandomTile()
    {
        // Calculate total weight with consecutive penalty applied
        float totalWeight = 0f;
        List<float> weights = new List<float>();

        for (int i = 0; i < groundTiles.Count; i++)
        {
            float weight = groundTiles[i].weight;

            // Apply penalty if this was the last spawned tile
            if (i == lastSpawnedIndex)
            {
                weight *= consecutivePenalty;
            }

            weights.Add(weight);
            totalWeight += weight;
        }

        // Select random tile based on weighted probability
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < weights.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        // Fallback (should never reach here)
        return groundTiles.Count - 1;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnTile();
        }
    }
}