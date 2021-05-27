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

    Material m;
    float altezza;
    public float inversoAltezzaFinale =  0;


    private void Start() {
        player = GameObject.FindWithTag("Player");
        layerMask = LayerMask.GetMask("Player");
        m = this.GetComponent<Renderer>().material;
        altezza = m.GetFloat("Vector1_e13e019d51d54a858419bc043499bafd");
    }

    private void Update() {
        if (dead) return;
        timer += Time.deltaTime;
        if (timer >= 0.75*timeout)
        {
            Debug.Log("Mod");
            m.SetFloat("Vector1_e13e019d51d54a858419bc043499bafd", Mathf.Lerp(altezza, inversoAltezzaFinale, ((timer - 0.75f * timeout) / (timeout - 0.75f))));
        }
        if (timer >= timeout && !hit) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, layerMask);
            if (hitColliders.Length > 0 && hit == false) {
                hit = true;
                player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.sprintSpeed /= slowDown;
                player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.runningSpeed /= slowDown;
                player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.walkSpeed /= slowDown;
                StartCoroutine(resetSpeed(timeout, effectDuration));
            }
            else Destroy(this.gameObject);
            dead = true;
            m.SetFloat("Vector1_e13e019d51d54a858419bc043499bafd", 1.0f);
        }
    }

    IEnumerator resetSpeed(float timeout, float slowDown) {
        GameObject playerCO = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(timeout);
        player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.sprintSpeed *= slowDown;
        player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.runningSpeed *= slowDown;
        player.GetComponent<Invector.vCharacterController.vThirdPersonController>().freeSpeed.walkSpeed *= slowDown;
        hit = false;
        Destroy(this.gameObject);
    }
}
