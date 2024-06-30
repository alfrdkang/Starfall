/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 26/06/2024
 * Date Modified: 26/06/2024
 * Description: Enemy Random Spawner Script
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns objects randomly at designated spawn points.
/// </summary>
public class RandomSpawner : MonoBehaviour
{
    public string spawnerTag = "spwner"; // Tag used to identify spawn points
    public bool alwaysSpawn = true; // Flag indicating whether to always spawn objects

    public List<GameObject> prefabsToSpawn; // List of prefabs to spawn

    private void Start()
    {
        // Find all game objects with the specified tag
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnerTag);

        // Iterate through each spawn point
        foreach (GameObject spawnPoint in spawnPoints)
        {
            // Randomly select a prefab from the list
            int randomPrefab = Random.Range(0, prefabsToSpawn.Count);

            // Check if alwaysSpawn is true or randomly decide whether to spawn
            if (alwaysSpawn || Random.Range(0, 4) == 0)
            {
                // Instantiate the selected prefab at the spawn point's position
                GameObject spawnedObject = Instantiate(prefabsToSpawn[randomPrefab]);
                spawnedObject.transform.position = spawnPoint.transform.position;
            }
        }
    }
}
