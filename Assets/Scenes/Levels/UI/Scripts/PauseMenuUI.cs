using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    private GameManager gameManager;
    private LevelManager levelManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    public void ResumeButton()
    {
        gameManager.Resume();
    }
}