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
        if (TowerCandidate == false)
        {
            TowerController tower = Instantiate(towerPrefab, gameObject.transform);
            TowerCandidate = tower;
        }
    }
    public void CancelTowerCandidate()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && TowerCandidate == true)
        {
            TowerCandidate.Cancel();
            TowerCandidate = null;
        }
    }
    public void ChangeTowerCandidate()
    {
        if (TowerCandidate == true)
        {
            TowerCandidate.Cancel();
            TowerCandidate = null;
        }
    }
    protected virtual void Update()
    {
        CancelTowerCandidate();

        if (Input.GetKeyUp(KeyCode.Mouse0) == true)
        {
            TryPlaceTowerCandidate();
        }
    }
    private void TryPlaceTowerCandidate()
    {
        if (TowerCandidate == false)
        {
            return;
        }
        if (TowerCandidate.CheckIfCanBePlaced() == true && GameManager.Instance.TryBuyTower(TowerCandidate) == true)
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
