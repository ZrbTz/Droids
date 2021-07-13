using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostMenuUI : MonoBehaviour
{
    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RestartLevelButton()
    {
        levelManager.ReloadScene();
    }

    public void ExitGameButton()
    {
        levelManager.LoadStart();
    }
}
