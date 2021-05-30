using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct LevelData {
    public string displayName;
    public string sceneName;
    public int level;
}

public class LevelMenu : MonoBehaviour {
    public List<LevelData> levelDatas;
    public LevelButton buttonPrefab;

    private void Start() {
        int playerLevel = PlayerPrefs.GetInt("Level", 0);
        foreach (LevelData levelData in levelDatas) {
            if (levelData.level <= playerLevel) {
                LevelButton button = Instantiate(buttonPrefab, transform);
                button.Init(levelData, playerLevel);
            }
        }
    }

    public void StartLevelOne() {
        SceneManager.LoadScene(1); //For the moment i just load level1
    }

    public void StartScene(string name) {
        SceneManager.LoadScene(name);
    }
}