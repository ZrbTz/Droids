using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberShootSMB : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.transform.root.GetComponent<NewBomber>().StartMarching();
    }
}

