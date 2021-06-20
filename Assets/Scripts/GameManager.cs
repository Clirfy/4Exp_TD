using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnCoinsAmmountChanged = delegate { };
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

    public bool TryBuyTower(TowerController tower)
    {
        int towerCost = tower.TowerCost;

        if (towerCost <= Money)
        {
            Money -= towerCost;
            OnCoinsAmmountChanged.Invoke();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(int moneyAmmount)
    {
        Money += moneyAmmount;
        OnCoinsAmmountChanged.Invoke();
    }

    public int TakeDamage(int damage)
    {
        return Hp -= damage;
    }

    public void GameOver()
    {
        if (Hp <= 0)
        {
            GameOverImage.SetActive(true);
        }
    }
}
