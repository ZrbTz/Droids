using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    public TextMeshProUGUI text;
    public Button button;

    private LevelData levelData;

    public void Init(LevelData levelData, int playerLevel) {
        this.levelData = levelData;
        button.interactable = (levelData.level <= playerLevel);
        text.text = levelData.displayName;
    }

    public void StartLevel() {
        SceneManager.LoadScene(levelData.sceneName);
    }
}
