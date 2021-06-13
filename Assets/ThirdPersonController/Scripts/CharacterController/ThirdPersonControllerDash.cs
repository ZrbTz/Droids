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
    private float dashRemainingStopTime = 0.0f;
    bool stopDash = false;
    private int callCount;
    private bool grounded;
    private bool strafing;
    private bool isThereInput;

    private Vector3 oldVelocity;

    private GameUI gameUI;

    void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        controller = this.GetComponent<vThirdPersonController>();
        callCount = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashRemainingTime > 0.0f && (strafing && !isThereInput)){
            //controller.isJumping = true;
            controller.isDashing = true;
            //direction = rb.transform.forward;
            rb.velocity = rb.velocity.normalized * dashSpeed;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            //rb.AddForce(direction * 1000f);
        }
        else if (dashRemainingTime > 0.0f)
        {
            //controller.isJumping = true;
            controller.isDashing = true;
            direction = rb.transform.forward;
            rb.velocity = direction * dashSpeed;
            //rb.AddForce(direction * 1000f);
        }
        else if (stopDash)
        {
            direction = rb.velocity.normalized;
            rb.velocity = direction * Mathf.Lerp(dashSpeed, oldVelocity.magnitude, dashRemainingStopTime / dashStopTime);
            dashRemainingStopTime += Time.deltaTime;
            if (dashRemainingStopTime >= dashStopTime)
            {
                stopDash = false;
                controller.isDashing = false;
                rb.velocity = oldVelocity.magnitude * direction;
            }
        }
        dashRemainingTime -= Time.deltaTime;
        dashRemainingCountdown -= Time.deltaTime;
        if (dashRemainingCountdown < 0)
        {
            callCount++;
            dashRemainingCountdown = dashTime + dashCountdown;
            if (callCount > 2)
            {
                callCount = 2;
            }
            else if (callCount < 2)
            {
                gameUI.UpdateDashCooldown(dashRemainingCountdown);
            }
            gameUI.UpdateDashNumber(callCount);
        }
    }

    public void Dash()
    {
        if (callCount > 0 && !controller.isDashing)
        {
            callCount--;
            stopDash = true;
            dashRemainingStopTime = 0.0f;
            dashRemainingTime = dashTime;

            dashRemainingCountdown = dashTime + dashCountdown;
            gameUI.UpdateDashCooldown(dashRemainingCountdown);
            strafing = controller.isStrafing;
            grounded = controller.isGrounded;
            isThereInput = (controller.input == Vector3.zero);

            gameUI.UpdateDashNumber(callCount);

            oldVelocity = rb.velocity;
            this.GetComponent<PlayerAnimationSounds>().Dash();
        }
    }
}
