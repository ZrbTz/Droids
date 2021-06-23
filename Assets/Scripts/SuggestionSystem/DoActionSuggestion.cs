using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DoActionSuggestion", menuName = "Suggestion System/Do Action Suggestion")]
public class DoActionSuggestion : Suggestion {

    [SerializeField] public string buttonName; //Button name
    [SerializeField] public int timeToPress; //How many times to press that button to complete the mission

    private int pressCounter; 

    public override bool IsCompleted(GameObject player) {
        if (Input.GetButtonDown(buttonName)) pressCounter++;
        return pressCounter == timeToPress;
    }
}
