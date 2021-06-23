using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DoActionSuggestion", menuName = "Suggestion System/Do Action Suggestion")]
public class DoActionSuggestion : Suggestion {

    [SerializeField] public string buttonName; //Button name
    [SerializeField] public int timeToPress; //How many times to press that button to complete the mission

    public int pressCounter = 0; 

    public override bool IsCompleted(GameObject player) {
        if (Input.GetButtonDown(buttonName)) pressCounter++;
        if (pressCounter >= timeToPress) { pressCounter = 0; return true; }
        else return false;
    }
}
