using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class Pinball : MonoBehaviour
{

    enum pathElement { start, trigger, endpoint};
    [SerializeField] pathElement Tipologia = pathElement.start;
    //Solo per lo start
    [SerializeField] GameObject target;
    //Solo per gli endpoint
    [SerializeField] bool mirroredVersion;
    [SerializeField] GameObject percorso;
    //Solo per i middle
    [SerializeField] GameObject Triggerabile;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject player = other.gameObject;
        if (player.layer == 10)
        {
            Debug.Log("Dentro");
            PinballMotion pinball_player = player.GetComponent<PinballMotion>();
            ThirdPersonControllerDash dash_player = player.GetComponent<ThirdPersonControllerDash>();
            Rigidbody rb = player.GetComponent<Rigidbody>();
            vThirdPersonInput input_player = player.GetComponent<vThirdPersonInput>();
            vThirdPersonController controller = player.GetComponent<vThirdPersonController>();
            vThirdPersonCamera camera_controller = Camera.main.GetComponent<vThirdPersonCamera>();
            
            if (Tipologia == pathElement.endpoint)
            {
                controller.isJumping = false;
                controller.isGrounded = true;
                pinball_player.enabled = false;
                dash_player.enabled = true;
                input_player.enabled = true;
                controller.UpdateAnimator();
                pinball_player.direction = Vector3.zero;
                percorso.SetActive(true);
                if (mirroredVersion)
                {
                    percorso.transform.rotation = Quaternion.Euler(Vector3.zero);
                } else
                {
                    percorso.transform.rotation = Quaternion.Euler(Vector3.up*180);
                }
            }
            else
            {
                if (Tipologia == pathElement.start)
                {
                    controller.isJumping = true;
                    controller.isGrounded = false;
                    pinball_player.enabled = true;
                    dash_player.enabled = false;
                    input_player.enabled = false;
                    controller.animator.CrossFadeInFixedTime("JumpMove", .2f);
                    controller.UpdateAnimator();
                    Vector3 directionTarget = target.transform.position - player.transform.position;
                    directionTarget.y = 0;
                    directionTarget = directionTarget.normalized;
                    float angle = Mathf.Atan2(directionTarget.x, directionTarget.z) * 180 / Mathf.PI;
                    pinball_player.direction = directionTarget;
                    rb.transform.forward = directionTarget;
                    camera_controller.SetMouseX(angle);
                    camera_controller.SetMouseY(0.0f);
                    this.GetComponent<PinballSounds>().PlaySound();
                }
                else
                {
                    Triggerabile.GetComponent<EstensioneMolla>().state = 1;
                    Triggerabile.GetComponent<PinballSounds>().PlaySound();
                    pinball_player.direction = Vector3.down*0.00001f;
                }
            }
        }
    }
}
