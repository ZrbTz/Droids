using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBomb : MonoBehaviour {
    static private bool hit = false;
    private GameObject player;
    public float timeout = 2.0f;
    public float slowDown = 2.0f;
    public float effectDuration = 2.0f;

    private float timer = 0.0f;

    private int layerMask;

    private bool dead = false;


    private void Start() {
        player = GameObject.FindWithTag("Player");
        layerMask = LayerMask.GetMask("Player");
    }

    private void Update() {
        if (hit) return;
        if (dead) return;
        timer += Time.deltaTime;
        if (timer >= timeout) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, layerMask);
            if (hitColliders.Length > 0 && hit == false) {
                hit = true;
                player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.sprintSpeed /= slowDown;
                player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.runningSpeed /= slowDown;
                player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.walkSpeed /= slowDown;
                StartCoroutine(resetSpeed(timeout, effectDuration));
            }
            dead = true;
            Destroy(this.gameObject, effectDuration + 1f);
        }
    }

    IEnumerator resetSpeed(float timeout, float slowDown) {
        GameObject playerCO = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(timeout);
        player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.sprintSpeed *= slowDown;
        player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.runningSpeed *= slowDown;
        player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.walkSpeed *= slowDown;
        hit = false;
    }
}
