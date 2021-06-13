using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public string displayName;
    public string sceneName;
    public int level;
}

public class LevelManager : MonoBehaviour
{
    [Header("Levels Settings")]
    public List<LevelData> levelsData;

    [Header("Transition Settings")]
    [SerializeField] private Animator transitionPanel;
    [SerializeField] private float delay = 1.0f;

    public void LoadStart()
    {
        Load(0);
    }

    public void ReloadScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        Load(index);
    }

    public void Load(int i)
    {
        StartCoroutine(SceneTransition(i));
    }

    public void Load(string name)
    {
        StartCoroutine(SceneTransition(name));
    }

    public void LoadNextLevel()
    {
        string currentName = SceneManager.GetActiveScene().name;
        int currentLevel = -1;
        for (int i = 0; i < levelsData.Count; i++)
        {
            if (levelsData[i].sceneName == currentName)
            {
                currentLevel = i;
            }
        }

        string name = levelsData[currentLevel++].sceneName;
        Load(name);
    }

    IEnumerator SceneTransition(int i)
    {
        transitionPanel.SetTrigger("End");
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        SceneManager.LoadScene(i);
    }

    IEnumerator SceneTransition(string name)
    {
        transitionPanel.SetTrigger("End");
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        SceneManager.LoadScene(name);
    }

    public List<LevelData> GetLevelsData()
    {
        return levelsData;
    }
}
