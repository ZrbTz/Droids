using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokerCloseShellSMB : StateMachineBehaviour {

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<NewInvoker>().StartMarching();
    }
}
