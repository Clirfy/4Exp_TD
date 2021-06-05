using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : SingletonMonoBehaviour<TowerManager>
{
    [field: SerializeField]
    private List<TowerController> TowerRegistry { get; set; }
    private TowerController TowerCandidate { get; set; }

    public void TrySpawnTowerPrefab(TowerController towerPrefab)
    {
        if(TowerCandidate == false)
        {
            TowerController tower = Instantiate(towerPrefab, gameObject.transform);
            TowerCandidate = tower;
        }
    }
    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            TryPlaceTowerCandidate();
        }
    }
    private void TryPlaceTowerCandidate()
    {
        if(TowerCandidate == false)
        {
            return;
        }
        if(TowerCandidate.CheckIfCanBePlaced() == true)
        {
            PlaceTower();
        }
    }
    private void PlaceTower()
    {
        TowerRegistry.Add(TowerCandidate);
        TowerCandidate.Place();
        TowerCandidate = null;
    }
}
