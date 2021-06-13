using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    [System.Serializable]
    public class Spawner
    {
        public string name;
        public GameObject spawnerObj;
    }

    private static GameManager instance;
    private enum SpawnState { SPAWNING, READY, WAITING };
    private SpawnState state = SpawnState.READY;

    public int nextBigHorde = 0;
    private int emptySpawners = 0;

    public Unit nexus;
    //public KeyCode spawnKey;
    public Spawner[] spawners;
    public int numHordes;

    public GameDifficulty difficulty;
    public bool ignoreDifficulty = true;

    private GameUI gameUI;
    private bool isPaused;

    private void Awake()
    {
        Application.targetFrameRate = 1000;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        instance = this;

        difficulty = (GameDifficulty)Enum.Parse(typeof(GameDifficulty), PlayerPrefs.GetString("Difficulty", "Normal"));

        gameUI = FindObjectOfType<GameUI>();
        Resume();
    }
    public static GameManager Instance { get => instance; }

    public void resetHorde()
    {
        nextBigHorde--;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemies)
        {
            Destroy(g);
        }

        gameUI.UpdateHordeNumber(nextBigHorde);
    }

    public void gameLost()
    {
        gameUI.ShowLostMenu();
        Pause();
    }

    public void gameWon()
    {
        gameUI.ShowWonMenu();
        Pause();
    }

    private float searchCountDown = 1f;
    bool allKilled()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            if (GameObject.FindWithTag("Enemy") == null) return true;
            searchCountDown = 1f;
        }
        return false;
    }

    public void signalSpawnEnd()
    {
        emptySpawners++;
    }

    void Update()
    {
        if (state == SpawnState.SPAWNING)
        {
            if (emptySpawners == spawners.Length) state = SpawnState.WAITING;
        }

        if (state == SpawnState.WAITING)
        {
            if (allKilled())
            {
                state = SpawnState.READY;
                if (nextBigHorde >= numHordes) gameWon();
                Debug.Log("Press E to new horde");
            }
            else return;
        }

        if (state == SpawnState.READY && Input.GetButtonUp("Spawn"))
        {
            state = SpawnState.SPAWNING;
            emptySpawners = 0;
            foreach (Spawner spawner in spawners) spawner.spawnerObj.GetComponent<SpawnEnemy>().spawnHorde(nextBigHorde);
            nextBigHorde++;

            gameUI.UpdateHordeNumber(nextBigHorde);
        }
    }

    public void HandlePause()
    {
        if (isPaused)
        {
            Resume();
            gameUI.HidePauseMenu();
        }
        else
        {
            gameUI.ShowPauseMenu();
            Pause();
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;

        isPaused = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
