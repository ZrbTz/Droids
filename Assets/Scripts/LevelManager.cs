using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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
}
