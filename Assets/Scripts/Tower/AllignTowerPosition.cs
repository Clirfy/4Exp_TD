using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllignTowerPosition : MonoBehaviour
{
    [field: SerializeField]
    private Transform PlaceMarkPosition { get; set; }

    private void OnCollisionStay(Collision collision)
    {
        collision.transform.position = PlaceMarkPosition.position;
    }
}
