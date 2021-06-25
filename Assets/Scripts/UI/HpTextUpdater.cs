using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpTextUpdater : MonoBehaviour
{
    [field: SerializeField]
    private Text Text { get; set; }

    private void Start()
    {
        UpdateText(GameManager.Instance.Hp);
        GameManager.Instance.OnHpChanged += ListenOnHpChanged;
    }

    private void ListenOnHpChanged()
    {
        UpdateText(GameManager.Instance.Hp);
    }
    private void UpdateText(int ammount)
    {
        Text.text = "Health: " + ammount;
    }
}
