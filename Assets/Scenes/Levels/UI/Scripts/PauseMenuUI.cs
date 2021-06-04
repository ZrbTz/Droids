using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour{

    public static bool isPaused = false;
    
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused) resume();
            else pause();
        }
    }
    public static void pauseWithoutUI() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void unpauseWithoutUI() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resume(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void loadMenu() {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }


   


}
