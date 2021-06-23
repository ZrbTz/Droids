using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DoActionSuggestion", menuName = "Suggestion System/Do Action Suggestion")]
public class DoActionSuggestion : Suggestion {

    [SerializeField] public string buttonName; //Button name
    [SerializeField] public int timeToPress; //How many times to press that button to complete the mission
    [SerializeField] public float cooldown;

    public float timer;
    private int pressCounter;

    public void OnEnable() {
        timer = cooldown;
        pressCounter = 0;
    }

    public override bool IsCompleted(GameObject player) {
        timer += Time.deltaTime;
        if (Input.GetButtonDown(buttonName) && timer >= cooldown) { pressCounter++; timer = 0; }
        return pressCounter >= timeToPress;
    }
}
