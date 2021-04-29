using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawner {
    public string name;
    public GameObject spawnerObj;
}

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private enum SpawnState { SPAWNING, READY, WAITING };
    private SpawnState state = SpawnState.READY;

    private int nextBigHorde = 0;
    private int emptySpawners = 0;

    public Nexus nexus;
    public DeathMenu deathMenu;
    public KeyCode spawnKey;
    public Spawner[] spawners;
    public int numHordes;

    /*
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    */

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        instance = this;
    }
    public static GameManager Instance { get => instance; }

    public void gameLost() {
        deathMenu.showDeathMenu();
    }

    public void gameWon() {
        //Show victory menu, for the moment i show the death menu XD
        deathMenu.showDeathMenu();
    }

    private float searchCountDown = 1f;
    bool allKilled() {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f) {
            if (GameObject.FindWithTag("Enemy") == null) return true;
            searchCountDown = 1f;
        }
        return false;
    }

    public void signalSpawnEnd() {
        emptySpawners++;
    }

    void Update() {
        if(state == SpawnState.SPAWNING) {
            if (emptySpawners == spawners.Length) state = SpawnState.WAITING;
        }

        if (state == SpawnState.WAITING) {
            if (allKilled()) {
                state = SpawnState.READY;
                if (nextBigHorde >= numHordes) gameWon();
                Debug.Log("Press E to new horde");
            }
            else return;
        }

        if (state == SpawnState.READY && Input.GetKeyUp(spawnKey)) {
            state = SpawnState.SPAWNING;
            emptySpawners = 0;
            foreach (Spawner spawner in spawners) spawner.spawnerObj.GetComponent<SpawnEnemy>().spawnHorde(nextBigHorde);
            nextBigHorde++;
        }
    }
}
