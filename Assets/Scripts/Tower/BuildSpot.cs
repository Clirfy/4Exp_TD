using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public bool IsOccupied { get; set; }

    public void SetAsOccupied()
    {
        IsOccupied = true;
    }
}
