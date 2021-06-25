using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public event Action OnCoinsAmmountChanged = delegate { };
    public event Action OnHpChanged = delegate { };

    [field: SerializeField]
    public int Hp { get; set; }
    [field: SerializeField]
    public int Money { get; set; }
    [field: SerializeField]
    private GameObject GameOverImage { get; set; }
    [field: SerializeField]
    private GameObject PauseImage { get; set; }
    private bool IsGamePaused { get; set; } = false;

    private void Update()
    {
        GameOver();
        Pause();
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

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        OnHpChanged.Invoke();
    }

    public void GameOver()
    {
        if (Hp <= 0)
        {
            GameOverImage.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            IsGamePaused = !IsGamePaused;
        }

        if(IsGamePaused == true)
        {
            PauseImage.SetActive(true);
        }
        else
        {
            PauseImage.SetActive(false);
        }
    }
}
