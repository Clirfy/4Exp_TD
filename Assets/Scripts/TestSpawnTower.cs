using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnTower : MonoBehaviour
{
    [field: SerializeField]
    private TowerController TowerPrefab { get; set; }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            TowerManager.Instance.TrySpawnTowerPrefab(TowerPrefab);
        }
    }
}
