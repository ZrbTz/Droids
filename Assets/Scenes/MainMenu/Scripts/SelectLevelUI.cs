using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelUI : StartMenuUI
{
    private List<LevelData> levelsData;
    private LevelManager levelManager;
    public LevelButton buttonPrefab;

    public Vector3 initPosition = new Vector3(9, -67, 0);

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        levelsData = levelManager.GetLevelsData();
        int playerLevel = PlayerPrefs.GetInt("Level", 0);
        foreach (LevelData levelData in levelsData)
        {
            LevelButton button = Instantiate(buttonPrefab, transform);
            button.transform.localPosition = initPosition;
            button.Init(levelData, playerLevel);

            initPosition.y -= 88;
        }
    }
}