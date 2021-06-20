using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> EnemySpawnedCollection { get; set; }
    [field: SerializeField]
    private Transform SpawnPosition { get; set; }

    [field: SerializeField]
    private List<EnemyController> EnemyPrefab { get; set; } = new List<EnemyController>();

    [field: SerializeField]
    private Transform DestinationPosition { get; set; }
    [field: SerializeField]
    private int MaxEnemiesNumber { get; set; }
    [field: SerializeField]
    private float MaxSpawnRate { get; set; }
    [field: SerializeField]
    private float MinSpawnRate { get; set; }
    [field: SerializeField]
    private float TimeToReachMinSpawnRate { get; set; }
    [field: SerializeField]
    private float CurrentSpawnRate { get; set; }
    private float TimeSinceLastSpawn { get; set; }

    private void Awake()
    {
        EnemySpawnedCollection = new List<EnemyController>();
        CurrentSpawnRate = MaxSpawnRate;
    }

    private void Update()
    {
        TimeSinceLastSpawn += Time.deltaTime;

        SpawnEnemyManualy();
        DestroySpawnedEnemies();

        if(EnemySpawnedCollection.Count < MaxEnemiesNumber && TimeSinceLastSpawn >= CurrentSpawnRate)
        {
            SpawnEnemy();
            TimeSinceLastSpawn = 0.0f;
        }
        if(CurrentSpawnRate > MinSpawnRate)
        {
            CurrentSpawnRate = Mathf.Lerp(MaxSpawnRate, MinSpawnRate, Time.time / TimeToReachMinSpawnRate);
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemyToSpawn = Random.Range(0, EnemyPrefab.Count);
        EnemyController enemy = Instantiate(EnemyPrefab[randomEnemyToSpawn], SpawnPosition);
        enemy.Initialize(DestinationPosition.position);

        EnemySpawnedCollection.Add(enemy);
        enemy.OnEnemyDestroy.AddListener(UnregisterEnemy);
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
