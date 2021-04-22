using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    /* MOVEMENT INPUTS */
    public bool isMoving { get; private set; }
    public Vector3 movementDirection { get; private set; }
    public float horizontalInput { get; private set; }
    public float verticalInput { get; private set; }

    /* JUMPING INPUTS */
    public bool jumped { get; private set; }

    void Update()
    {
        CheckMovement();
        CheckJump();
    }

    private void CheckMovement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movementDirection.magnitude >= 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumped = true;
        }
        else
        {
            jumped = false;
        }
    }
}
