using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class Pinball : MonoBehaviour
{
    public bool start;
    public bool end;
    public bool inverted;

    public GameObject percorso;

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
            Rigidbody player_rb = player.GetComponent<Rigidbody>();
            ThirdPersonControllerDash dash_player = player.GetComponent<ThirdPersonControllerDash>();
            vThirdPersonInput input_player = player.GetComponent<vThirdPersonInput>();
            vThirdPersonCamera camera_controller = Camera.main.GetComponent<vThirdPersonCamera>();
            if (start)
            {
                pinball_player.enabled = true;
                dash_player.enabled = false;
                input_player.enabled = false;
            }
            pinball_player.direction = this.gameObject.transform.forward;
            if (end)
            {
                pinball_player.enabled = false;
                dash_player.enabled = true;
                input_player.enabled = true;
                pinball_player.direction = Vector3.zero;
                percorso.SetActive(true);
                if (inverted)
                {
                    percorso.transform.rotation = Quaternion.Euler(Vector3.zero);
                } else
                {
                    percorso.transform.rotation = Quaternion.Euler(Vector3.up*180);
                }
            }
            pinball_player.direction = this.gameObject.transform.forward;
            if(!end)
            {
                float angle = Mathf.Atan2(this.gameObject.transform.forward.x, this.gameObject.transform.forward.z)*180/Mathf.PI;
                camera_controller.SetMouseX(angle); 
                camera_controller.SetMouseY(0.0f);
            }
        }
    }
}
