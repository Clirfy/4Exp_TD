using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowSpell : MonoBehaviour
{
    [field: SerializeField]
    private string ButtonSpellName { get; set; }
    [field: SerializeField]
    private Button ThisButton { get; set; }
    [field: SerializeField]
    private Text ButtonText { get; set; }
    [field: SerializeField]
    private BaseSpellController SpellPrefab { get; set; }
    private float CooldownTimer { get; set; }

    private void Awake()
    {
        CooldownTimer = 0.0f;
    }

    private void Update()
    {
        CooldownTimer -= Time.deltaTime;

        if(CooldownTimer <= 0.0f)
        {
            ThisButton.interactable = true;
            ButtonText.text = ButtonSpellName;
        }
        else
        {
            ThisButton.interactable = false;
            ButtonText.text = CooldownTimer.ToString("F0");
        }
    }

    public void TrySpawnSpellPrefab()
    {
        BaseSpellController spell = Instantiate(SpellPrefab, gameObject.transform);
        spell.OnSpellCasted += ListenOnSpellCasted;
    }

    private void ListenOnSpellCasted()
    {
        CooldownTimer = SpellPrefab.Cooldown;
    }
}
