using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button button;

    private LevelData levelData;

    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void Init(LevelData levelData, int playerLevel)
    {
        this.levelData = levelData;
        button.interactable = (levelData.level <= playerLevel);
        text.text = levelData.displayName;
    }

    public void StartLevel()
    {
        levelManager.Load(levelData.sceneName);
    }
}