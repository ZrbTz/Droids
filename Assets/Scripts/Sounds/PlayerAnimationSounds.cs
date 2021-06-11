using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class PlayerAnimationSounds : MonoBehaviour
{
    [SerializeField] AudioClip walkStep;
    [SerializeField] AudioClip walkMovement;
    [SerializeField] AudioClip jumpStart;
    [SerializeField] AudioClip singleShot;
    [SerializeField] AudioClip dash;

    private AudioSource _speaker;
    private AudioSource mov_speaker;
    private vThirdPersonController _controller;

    // Start is called before the first frame update
    void Start()
    {
        mov_speaker = this.gameObject.AddComponent<AudioSource>();
        mov_speaker.playOnAwake = false;
        mov_speaker.volume = 0.04f;
        _speaker = this.GetComponent<AudioSource>();
        _controller = this.GetComponent<vThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Step
    public void StepForward()
    {
        Debug.Log("StepF");
        if (_controller.input.z > 0.01f && Mathf.Abs(_controller.input.x) < 0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepForwardSx()
    {
        Debug.Log("StepFS");
        if (_controller.input.z > 0.01f && _controller.input.x <-0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepForwardDx()
    {
        Debug.Log("StepFD");
        if (_controller.input.z > 0.01f && _controller.input.x > 0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepSx()
    {
        Debug.Log("StepS");
        if (Mathf.Abs(_controller.input.z) < 0.01f && _controller.input.x < -0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepDx()
    {
        Debug.Log("StepD");
        if (Mathf.Abs(_controller.input.z) < 0.01f && _controller.input.x > 0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackwardSx()
    {
        Debug.Log("StepBS");
        if (_controller.input.z < -0.01f && _controller.input.x > 0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackwardDx()
    {
        Debug.Log("StepBD");
        if (_controller.input.z < -0.01f && _controller.input.x < -0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackward()
    {
        Debug.Log("StepB");
        if (_controller.input.z < -0.01f && Mathf.Abs(_controller.input.x) < 0.01f && _controller.isJumping == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    #endregion
    public void Run()
    {
        Debug.Log("Run");
        if (_controller.inputMagnitude > 0.01f && _controller.isJumping==false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void Movement()
    {
        Debug.Log("Movement");
        if (_controller.inputMagnitude > 0.01f)
        {
            mov_speaker.PlayOneShot(walkMovement);

        }
    }
    public void Jump()
    {
        _speaker.PlayOneShot(jumpStart);
    }
    public void Landing()
    {
        _speaker.PlayOneShot(walkStep);
    }

    public void Shoot(AudioSource shooter_speaker)
    {
        shooter_speaker.PlayOneShot(singleShot);
    }

    public void Dash()
    {
        _speaker.PlayOneShot(dash);
    }
}
