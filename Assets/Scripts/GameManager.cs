using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    public Nexus nexus;
    public DeathMenu deathMenu;

    private void Awake() {
        instance = this;
    }

    public static GameManager Instance { get => instance; }

    public void gameLost() {
        deathMenu.showDeathMenu();
    }
}
