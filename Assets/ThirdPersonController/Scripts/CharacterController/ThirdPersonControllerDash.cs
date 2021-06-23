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
    private bool sprinting;
    private Vector3 input;
    private Vector3 forwardDirection;
    private Vector3 rightDirection;

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
        if (dashRemainingTime > 0.0f && (!sprinting && (input != Vector3.zero))){
            controller.isDashing = true;
            rb.velocity = (forwardDirection * input.normalized.z + rightDirection * input.normalized.x)*dashSpeed;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        else if (dashRemainingTime > 0.0f)
        {
            controller.isDashing = true;
            rb.velocity = forwardDirection * dashSpeed;
        }
        else if (stopDash)
        {
            direction = rb.velocity.normalized;
            rb.velocity = direction * Mathf.Lerp(dashSpeed, oldVelocity.magnitude, dashRemainingStopTime / dashStopTime);
            dashRemainingStopTime += Time.fixedDeltaTime;
            if (dashRemainingStopTime >= dashStopTime)
            {
                stopDash = false;
                controller.isDashing = false;
                rb.velocity = oldVelocity.magnitude * direction;
            }
        }
        dashRemainingTime -= Time.fixedDeltaTime;
        dashRemainingCountdown -= Time.fixedDeltaTime;
        if (callCount < 2 && dashRemainingCountdown < 0)
        {
            callCount++;
            gameUI.UpdateDashNumber(callCount);
            
            if (callCount < 2)
            {
                dashRemainingCountdown = dashTime + dashCountdown;
                gameUI.UpdateDashCooldown(dashRemainingCountdown);
            }

        }
    }

    public void Dash()
    {
        if (callCount > 0 && !controller.isDashing)
        {
            if (SuggestionController.Instance.actions.TryGetValue("Dash", out DoActionSuggestion action)) action.incrementPressCounter();
            callCount--;
            stopDash = true;
            dashRemainingStopTime = 0.0f;
            dashRemainingTime = dashTime;

            if (dashRemainingCountdown <  0)
            {
                dashRemainingCountdown = dashTime + dashCountdown;
            }
            
            gameUI.UpdateDashCooldown(dashRemainingCountdown);
            sprinting = controller.isSprinting;
            input = (controller.input);
            forwardDirection = rb.transform.forward;
            rightDirection = rb.transform.right;
            
            gameUI.UpdateDashNumber(callCount);

            oldVelocity = rb.velocity;
            this.GetComponent<PlayerAnimationSounds>().Dash();
        }
    }


}
