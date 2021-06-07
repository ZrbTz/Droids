using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public string displayName;
    public string sceneName;
    public int level;
}

public class SelectLevelUI : StartMenuUI
{
    public List<LevelData> levelDatas;
    public LevelButton buttonPrefab;

    public Vector3 initPosition = new Vector3(9, -67, 0);

    private void Start()
    {
        int playerLevel = PlayerPrefs.GetInt("Level", 0);
        foreach (LevelData levelData in levelDatas)
        {
            LevelButton button = Instantiate(buttonPrefab, transform);
            button.transform.localPosition = initPosition;
            button.Init(levelData, playerLevel);

            initPosition.y -= 88;
        }
    }
}