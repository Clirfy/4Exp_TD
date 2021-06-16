using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> EnemySpawnedCollection { get; set; } = new List<EnemyController>();
    [field: SerializeField]
    private Transform SpawnPosition { get; set; }

    [field: SerializeField]
    private List<EnemyController> EnemyPrefab { get; set; } = new List<EnemyController>();

    [field: SerializeField]
    private Transform DestinationPosition { get; set; }
    [field: SerializeField]
    private float MinSpawnRate { get; set; }
    [field: SerializeField]
    private float MaxSpawnRate { get; set; }
    [field: SerializeField]
    private float SpawnRateTimer { get; set; }
    [field: SerializeField]
    private float SpawnRate { get; set; }
    [field: SerializeField]
    private float FastenSpawnRateInterval { get; set; }

    private void Awake()
    {
        SpawnRateTimer = MinSpawnRate;
        SpawnRate = MinSpawnRate;
    }

    private void Update()
    {
        SpawnEnemyAutomaticly();
        SpawnEnemyManualy();
        DestroySpawnedEnemies();
    }

    private void SpawnEnemy()
    {
        int randomEnemyToSpawn = Random.Range(0, EnemyPrefab.Count);
        EnemyController enemy = Instantiate(EnemyPrefab[randomEnemyToSpawn], SpawnPosition);
        enemy.Initialize(DestinationPosition.position);

        EnemySpawnedCollection.Add(enemy);
        enemy.OnEnemyDestroy.AddListener(UnregisterEnemy);
    }

    private void SpawnEnemyAutomaticly()
    {
        SpawnRateTimer -= Time.deltaTime;

        if (SpawnRateTimer <= 0)
        {
            SpawnRate -= FastenSpawnRateInterval;
            SpawnRate = Mathf.Clamp(SpawnRate, MaxSpawnRate, MinSpawnRate);
            SpawnRateTimer = SpawnRate;

            SpawnEnemy();
        }
    }

    private void SpawnEnemyManualy()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEnemy();
        }
    }

    private void DestroySpawnedEnemies()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //var enemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (EnemyController enemy in EnemySpawnedCollection)
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    private void UnregisterEnemy(EnemyController enemy)
    {
        //Debug.Log("Enemy Unregistered" + enemy);
        EnemySpawnedCollection.Remove(enemy);
        enemy.OnEnemyDestroy.RemoveListener(UnregisterEnemy);
    }
}
