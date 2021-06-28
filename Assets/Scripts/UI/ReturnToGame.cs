using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToGame : MonoBehaviour
{
    public void Return()
    {
        GameManager.Instance.IsGamePaused = false;
    }
}
