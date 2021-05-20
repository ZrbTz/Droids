using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class PinballMotion: MonoBehaviour
{
    public Vector3 direction;
    public float velocity = 10.0f;
    public float height_limit = 0.3f;

    private Rigidbody rb;
    private vThirdPersonController controller;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        controller = this.GetComponent<vThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.isJumping = true;
        controller.isGrounded = false;
        rb.velocity = direction * velocity;
        rb.transform.forward = direction;

        if (this.gameObject.transform.position.y > height_limit)
        {
            Vector3 pos = this.gameObject.transform.position;
            pos.y = height_limit;
            this.gameObject.transform.position = pos;
        }
    }
}
