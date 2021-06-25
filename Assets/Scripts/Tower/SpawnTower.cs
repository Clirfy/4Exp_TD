using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    [field: SerializeField]
    private TowerController TowerPrefab { get; set; }

    public void SpawnTowerPrefab()
    {
        TowerManager.Instance.ChangeTowerCandidate();
        TowerManager.Instance.TrySpawnTowerPrefab(TowerPrefab);
    }
}
