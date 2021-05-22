using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonePinball : MonoBehaviour
{
    [SerializeField] GameObject riferimento;
    [SerializeField] GameObject molla;
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = riferimento.transform.position + Vector3.forward*this.transform.localScale.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            GameObject player = collision.gameObject;
            PinballMotion pinball_player = player.GetComponent<PinballMotion>();
            vThirdPersonCamera camera_controller = Camera.main.GetComponent<vThirdPersonCamera>();
            Rigidbody rb = player.GetComponent<Rigidbody>();
            Vector3 directionTarget = target.transform.position - player.transform.position;
            directionTarget.y = 0;
            directionTarget = directionTarget.normalized;
            float angle = Mathf.Atan2(directionTarget.x, directionTarget.z) * 180 / Mathf.PI;
            pinball_player.direction = directionTarget;
            rb.transform.forward = directionTarget;
            camera_controller.SetMouseX(angle);
            camera_controller.SetMouseY(0.0f);
            molla.GetComponent<EstensioneMolla>().state = 0;
            Vector3 scale = molla.transform.localScale;
            scale.z = 0.1f;
            molla.transform.localScale = scale;
        }
    }
}
