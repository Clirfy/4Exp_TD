using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowSpell : MonoBehaviour
{
    [field: SerializeField]
    private Button ThisButton { get; set; }
    [field: SerializeField]
    private BaseSpellController SpellPrefab { get; set; }
    private float CooldownTimer { get; set; }

    private void Awake()
    {
        CooldownTimer = SpellPrefab.Cooldown;
    }

    private void Update()
    {
        CooldownTimer += Time.deltaTime;

        if(CooldownTimer >= SpellPrefab.Cooldown)
        {
            ThisButton.interactable = true;
        }
        else
        {
            ThisButton.interactable = false;
        }
    }

    public void TrySpawnSpellPrefab()
    {
        BaseSpellController spell = Instantiate(SpellPrefab, gameObject.transform);
        spell.OnSpellCasted += ListenOnSpellCasted;
    }

    private void ListenOnSpellCasted()
    {
        CooldownTimer = 0.0f;
    }
}
