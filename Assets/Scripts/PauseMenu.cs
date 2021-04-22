using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused) resume();
            else pause();
        }
    }
    public static void pauseWithoutUI() {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void unpauseWithoutUI() {
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void loadMenu() {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }


   


}
