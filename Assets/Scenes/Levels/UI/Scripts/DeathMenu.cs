using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour{

    public GameObject deathMenuUI;
    public void showDeathMenu() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //PauseMenuUI.pauseWithoutUI();
        deathMenuUI.SetActive(true);
    }

    public void restartLevel() {
        deathMenuUI.SetActive(false);
        //PauseMenuUI.unpauseWithoutUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void loadMenu() {
        deathMenuUI.SetActive(false);
        //PauseMenuUI.unpauseWithoutUI();
        SceneManager.LoadScene(0);
    }


}
