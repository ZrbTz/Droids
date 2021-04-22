using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour{

    public GameObject deathMenuUI;
    public void showDeathMenu() {
        PauseMenu.pauseWithoutUI();
        deathMenuUI.SetActive(true);
    }

    public void restartLevel() {
        deathMenuUI.SetActive(false);
        PauseMenu.unpauseWithoutUI();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void loadMenu() {
        deathMenuUI.SetActive(false);
        PauseMenu.unpauseWithoutUI();
        SceneManager.LoadScene(0);
    }


}
