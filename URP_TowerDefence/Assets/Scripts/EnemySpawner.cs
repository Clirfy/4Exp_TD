using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField]
    private Transform SpawnPosition { get; set; }

    [field: SerializeField]
    private GameObject EnemyPrefab { get; set; }

    [field: SerializeField]
    private Transform DestinationPosition { get; set; }


    private void Update()
    {
        SpawnEnemy();
        DestroySpawnedEnemies();
    }

    private void SpawnEnemy()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject enemy = Instantiate(EnemyPrefab, SpawnPosition);
            enemy.GetComponent<EnemyController>().Initialize(SpawnPosition.position, DestinationPosition.position);
        }
    }

    private void DestroySpawnedEnemies()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            var enemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var item in enemy)
            {
                Destroy(item);
            }
        }
    }
}
