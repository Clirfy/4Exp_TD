using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelSelection : MonoBehaviour
{
    [field: SerializeField]
    private string LevelName { get; set; }

    public void LoadNewLevel()
    {
        Debug.Log("Loading " + LevelName);
        FindObjectOfType<UI>().LoadNewScene(LevelName);
    }

    public void DebugLevelName()
    {
        Debug.Log(LevelName);
    }
}
