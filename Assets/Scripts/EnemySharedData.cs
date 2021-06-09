using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySharedData", menuName = "ScriptableObjects/EnemySharedData")]
public class EnemySharedData : ScriptableObject
{
    [field: SerializeField]
    public LayerMask DestinationLayerMask{ get; set;}
}
