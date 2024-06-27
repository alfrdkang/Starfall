/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 26/06/2024
 * Date Modified: 26/06/2024
 * Description: Enemy Random Spawner Script
 */

using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public string spawnerTag = "spwner";
    public bool alwaysSpawn = true;

    public List<GameObject> prefabsToSpawn;

    private void Start()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnerTag);
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int randomPrefab = Random.Range(0, prefabsToSpawn.Count);
            if (alwaysSpawn)
            {
                GameObject pts = Instantiate(prefabsToSpawn[randomPrefab]);
                pts.transform.position = spawnPoint.transform.position;
            }
            else
            {
                int spawnOrNot = Random.Range(0, 2);
                if (spawnOrNot == 0)
                {
                    GameObject pts = Instantiate(prefabsToSpawn[randomPrefab]);
                    pts.transform.position = spawnPoint.transform.position;
                }
            }
        }
    }

}