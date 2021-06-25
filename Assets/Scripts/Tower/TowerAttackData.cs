using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data", menuName = "Turrets/Attack Data")]
public class TowerAttackData : ScriptableObject
{
    [field: SerializeField]
    public int AttackDamage { get; set; }
    [field: SerializeField]
    public int AttackRange { get; set; }
    [field: SerializeField]
    public float AttackRatio { get; set; }
}
