using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//To be used also to give indication about specific enemies.
[CreateAssetMenu(fileName = "New CompleteHordeSuggestion", menuName = "Suggestion System/Complete Horde Suggestion")]
public class CompleteHordeSuggestion : Suggestion {

    [SerializeField] public int hordeToComplete; //Id of the horde to be completed
    private GameManager gm = GameManager.Instance; //Why doesn't it work?

    public override bool IsCompleted(GameObject player) {
        return GameManager.Instance.nextBigHorde > hordeToComplete && GameManager.Instance.getState() == 1;
    }
}
