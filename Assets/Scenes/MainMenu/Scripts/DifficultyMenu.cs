using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameDifficulty {
    Normal,
    Easy,
    Hard
}

public class DifficultyMenu : MonoBehaviour {
    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    private void OnEnable() {
        Invoke(nameof(HighlightButton), 0f);
    }

    private void HighlightButton() {
        GameDifficulty difficulty = (GameDifficulty)Enum.Parse(typeof(GameDifficulty), PlayerPrefs.GetString("Difficulty", "Normal"));
        switch (difficulty) {
            case GameDifficulty.Easy:
                easyButton.Select();
                break;
            case GameDifficulty.Normal:
                normalButton.Select();
                break;
            case GameDifficulty.Hard:
                hardButton.Select();
                break;
        }
    }

    public void SetEasyDifficulty() {
        PlayerPrefs.SetString("Difficulty", "Easy");
    }

    public void SetNormalDifficulty() {
        PlayerPrefs.SetString("Difficulty", "Normal");
    }

    public void SetHardDifficulty() {
        PlayerPrefs.SetString("Difficulty", "Hard");
    }
}