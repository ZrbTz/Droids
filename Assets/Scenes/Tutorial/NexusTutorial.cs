using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusTutorial : Unit
{
    private GameUI gameUI;

    void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    protected override void Die()
    {
        GameManager gm = GameManager.Instance;
        health = 5;
        dead = false;
        gm.resetHorde();

        gameUI.UpdateNotification("You lost, try to fight this wave again!");
    }
}
