using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class PinballMotion: MonoBehaviour
{
    public Vector3 direction;
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
        rb.velocity = direction * 10;
        if(this.gameObject.transform.position.y > 0.3f)
        {
            Vector3 pos = this.gameObject.transform.position;
            pos.y = 0.3f;
            this.gameObject.transform.position = pos;
        }
    }
}
