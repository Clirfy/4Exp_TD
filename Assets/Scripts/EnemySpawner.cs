using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField]
    private Transform SpawnPosition { get; set; }

    [field: SerializeField]
    private EnemyController EnemyPrefab { get; set; }

    [field: SerializeField]
    private Transform DestinationPosition { get; set; }
    private List<EnemyController> EnemySpawnedCollection = new List<EnemyController>();

    private void Update()
    {
        SpawnEnemy();
        DestroySpawnedEnemies();
    }

    private void SpawnEnemy()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject enemy = Instantiate(EnemyPrefab.gameObject, SpawnPosition);
            enemy.GetComponent<EnemyController>().Initialize(SpawnPosition.position, DestinationPosition.position);

            EnemySpawnedCollection.Add(EnemyPrefab);
        }
    }

    private void DestroySpawnedEnemies()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //var enemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var item in EnemySpawnedCollection)
            {
                Destroy(item);
            }
        }
    }

    private void UnregisterEnemy(EnemyController enemy)
    {
        EnemySpawnedCollection.Remove(enemy);
        enemy.OnEnemyDestroyEvent.RemoveListener(UnregisterEnemy);
    }
}
