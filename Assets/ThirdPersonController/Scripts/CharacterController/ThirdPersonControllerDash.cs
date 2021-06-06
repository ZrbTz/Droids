using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class ThirdPersonControllerDash : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;
    private vThirdPersonController controller;
    public float dashSpeed = 15f;
    public float dashTime = 0.2f;
    private float dashRemainingTime = 0.0f;
    public float dashCountdown = 2.0f;
    private float dashRemainingCountdown = 0.0f;
    public float dashStopTime = 0.1f;
    private float dashRempainingStopTime = 0.0f;
    bool stopDash = false;
     
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        controller = this.GetComponent<vThirdPersonController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dashRemainingTime > 0.0f)
        {
            //TODO: brutto
            //controller.isJumping = true;
            Debug.Log("dashing");
            controller.isDashing = true;
            direction = rb.transform.forward;
            rb.velocity = direction * dashSpeed;
            //rb.AddForce(direction * 1000f);
        }
        else if (stopDash) {
            direction = rb.transform.forward;
            rb.velocity = direction * Mathf.Lerp(dashSpeed, 0.0f, dashRempainingStopTime / dashStopTime);
            dashRempainingStopTime += Time.deltaTime;
            if (dashRempainingStopTime >= dashStopTime) {
                stopDash = false;
                controller.isDashing = false;
            }
        }
        dashRemainingTime -= Time.deltaTime;
        dashRemainingCountdown -= Time.deltaTime;
    }

    public void Dash()
    {
        if(dashRemainingCountdown <= 0.0f)
        {
            stopDash = true;
            dashRempainingStopTime = 0.0f;
            dashRemainingTime = dashTime;
            dashRemainingCountdown = dashTime + dashCountdown;
        }
    }
}
