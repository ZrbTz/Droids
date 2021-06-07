using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public void LoadStart()
    {
        Load(0);
    }

    public void ReloadScene() {
        int index =  SceneManager.GetActiveScene().buildIndex;
        Load(index);
    }

    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }

    /*IEnumerator SceneTransition(int i)
    {
        transitionPanel.SetTrigger("End");
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        SceneManager.LoadScene(i);
    }*/
}
