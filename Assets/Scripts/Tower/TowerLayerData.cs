using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Layer Data", menuName = "Turrets/Layer Data")]
public class TowerLayerData : ScriptableObject
{
    [field: SerializeField]
    public LayerMask FloorLayerMask { get; set; }
    [field: SerializeField]
    public LayerMask BuildGroundLayerMask { get; set; }
    [field: SerializeField]
    public LayerMask EnemyLayerMask { get; set; }
    [field: SerializeField]
    public LayerMask TowerLayerMask { get; set; }

}
