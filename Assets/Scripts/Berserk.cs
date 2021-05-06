using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Invector.vCharacterController.vThirdPersonController>() != null) {
            other.GetComponent<AttackSystem>().switchMode();
            Destroy(this.gameObject);
        }
    }
}
