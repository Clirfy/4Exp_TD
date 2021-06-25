using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsTextUpdater : MonoBehaviour
{
    [field: SerializeField]
    private Text Text { get; set; }

    private void Start()
    {
        UpdateText(GameManager.Instance.Money);
        GameManager.Instance.OnCoinsAmmountChanged += ListenOnMoneyAmmountChanged;
    }

    private void ListenOnMoneyAmmountChanged()
    {
        UpdateText(GameManager.Instance.Money);
    }

    private void UpdateText(int ammount)
    {
        Text.text = "Money: " + ammount;
    }
}
