using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DoActionSuggestion", menuName = "Suggestion System/Do Action Suggestion")]
public class DoActionSuggestion : Suggestion {

    [SerializeField] public string actionName; //Button name
    [SerializeField] public int timeToPress; //How many times to press that button to complete the mission
    //[SerializeField] public float cooldown;

    //public float timer;
    public int pressCounter;

    public override void init() {
        //timer = cooldown;
        pressCounter = 0;
        SuggestionController.Instance.actions.Add(actionName, this);
    }

    public override void reset() {
        SuggestionController.Instance.actions.Remove(actionName);
    }

    public override bool IsCompleted(GameObject player) {
        //timer += Time.deltaTime;
        //if (Input.GetButtonDown(buttonName) && timer >= cooldown) { pressCounter++; /*timer = 0;*/ }
        //if (pressCounter >= timeToPress) SuggestionController.Instance.actions.Remove(actionName);
        return pressCounter >= timeToPress;
    }

    public void incrementPressCounter() {
        pressCounter++;
    }
}
