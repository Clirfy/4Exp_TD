using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [field: SerializeField]
    public int Hp { get; set; }
    [field: SerializeField]
    public int Money { get; set; }
    [field: SerializeField]
    private GameObject GameOverImage { get; set; }

    private void Update()
    {
        GameOver();
    }

    public void TryBuyTower(TowerController tower)
    {
        int towerCost = tower.TowerCost;

        if(towerCost <= Money)
        {
            Money -= towerCost;
        }
    }

    public int AddMoney(int moneyAmmount)
    {
        return Money += moneyAmmount;
    }

    public int TakeDamage(int damage)
    {
        return Hp -= Money;
    }

    public void GameOver()
    {
        if(Hp <= 0)
        {
            GameOverImage.SetActive(true);
        }
    }
}
