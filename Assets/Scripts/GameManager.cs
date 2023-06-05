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
    public int _coins;
    public int coins { 
        get => _coins; 
        set {
            CoinsChanged?.Invoke(_coins, value);
            _coins = value;
        } 
    }
    public Action<int, int> CoinsChanged;

    public GameDifficulty difficulty;
    public bool ignoreDifficulty = true;

    private GameUI gameUI;
    private bool isPaused;
    private bool isGameOver = false;

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
        Inventory inv = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject g in enemies)
        {
            Destroy(g);
        }

        NewNexus[] areaNexus = GameObject.FindObjectsOfType<NewNexus>();
        foreach(NewNexus n in areaNexus) n.StopAllCoroutines();

        if (nextBigHorde == 2) {
            Debug.Log("object destroy");
            GameObject tower = GameObject.FindWithTag("Tower");
            if(tower != null) Destroy(tower);
            inv.flush();
        }
        ProximityPickable[] grenades = GameObject.FindObjectsOfType<ProximityPickable>();
        int groundGrenades = grenades.Length;
        foreach (ProximityPickable t in grenades) Destroy(t.gameObject);

        if(nextBigHorde == 1 && inv.getAmountInSlot(1) > 0 ) {
            inv.DecreaseItemAmount(1);
        }


        foreach (Spawner s in spawners) s.spawnerObj.GetComponent<SpawnEnemy>().StopSpawning();
        state = SpawnState.WAITING;
        gameUI.UpdateHordeNumber(nextBigHorde);
        gameUI.UpdateNexusHealth(nexus.health, nexus.GetMaxHealth());
    }

    public void gameLost()
    {
        isGameOver = true;
        gameUI.ShowLostMenu();
        Pause();
    }

    public void gameWon()
    {
        isGameOver = true;
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
                if (nextBigHorde >= numHordes)
                {
                    gameWon();
                }
                else
                {
                    gameUI.ShowStartWave();
                }
            }
            else return;
        }

        if (state == SpawnState.READY && Input.GetButtonUp("Spawn"))
        {
            if (!SuggestionController.Instance.isBlocking()) {
                state = SpawnState.SPAWNING;
                if (SuggestionController.Instance.actions.TryGetValue("Spawn", out DoActionSuggestion action)) action.incrementPressCounter();
                emptySpawners = 0;
                foreach (Spawner spawner in spawners) spawner.spawnerObj.GetComponent<SpawnEnemy>().spawnHorde(nextBigHorde);
                nextBigHorde++;

                gameUI.UpdateHordeNumber(nextBigHorde);
                gameUI.HideStartWave();
            }
            else { 
                gameUI.UpdateNotification("Please, complete your suggestion first!");
            }
        }
    }

    public void HandlePause()
    {
        if (!isGameOver)
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

    public int getState() {
        return (int)state;
    }
}
