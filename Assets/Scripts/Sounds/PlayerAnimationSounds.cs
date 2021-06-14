using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class PlayerAnimationSounds : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] AudioClip walkStep;
    [SerializeField] AudioClip walkMovement;
    [SerializeField] [Range(0.0f, 1.0f)] float movementVolume = 0.04f;
    [SerializeField] AudioClip jumpStart;
    [SerializeField] AudioClip singleShot;
    [SerializeField] [Range(0.0f, 1.0f)] float singleShotVolume = 0.02f;
    [SerializeField] AudioClip shotgun;
    [SerializeField] [Range(0.0f, 1.0f)] float shotgunVolume = 0.09f;
    [SerializeField] AudioClip dash;

    private AudioSource _speaker;
    private AudioSource mov_speaker;
    private vThirdPersonController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _speaker = this.GetComponent<AudioSource>();
        mov_speaker = CreateSpeaker(movementVolume);
        _controller = this.GetComponent<vThirdPersonController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.IsPaused())
        {
            _speaker.Pause();
            mov_speaker.Pause();
        }
        else
        {
            _speaker.UnPause();
            mov_speaker.UnPause();
        }
    }

    private AudioSource CreateSpeaker(float intensity)
    {
        AudioSource newSpeaker = this.gameObject.AddComponent<AudioSource>();
        newSpeaker.volume = intensity;
        newSpeaker.playOnAwake = false;
        newSpeaker.spatialBlend = 1.0f;
        return newSpeaker;
    }

    #region Step
    public void StepForward()
    {
        //Debug.Log("StepF");
        if (_controller.input.z > 0.01f && Mathf.Abs(_controller.input.x) < 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepForwardSx()
    {
        //Debug.Log("StepFS");
        if (_controller.input.z > 0.01f && _controller.input.x <-0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepForwardDx()
    {
        //Debug.Log("StepFD");
        if (_controller.input.z > 0.01f && _controller.input.x > 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepSx()
    {
        //Debug.Log("StepS");
        if (Mathf.Abs(_controller.input.z) < 0.01f && _controller.input.x < -0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepDx()
    {
        //Debug.Log("StepD");
        if (Mathf.Abs(_controller.input.z) < 0.01f && _controller.input.x > 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackwardSx()
    {
        //Debug.Log("StepBS");
        if (_controller.input.z < -0.01f && _controller.input.x > 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackwardDx()
    {
        //Debug.Log("StepBD");
        if (_controller.input.z < -0.01f && _controller.input.x < -0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackward()
    {
        //Debug.Log("StepB");
        if (_controller.input.z < -0.01f && Mathf.Abs(_controller.input.x) < 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    #endregion
    public void Run()
    {
        //Debug.Log("Run");
        if (_controller.inputMagnitude > 0.01f && _controller.isJumping==false && _controller.isSprinting == true)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void Movement()
    {
        //Debug.Log("Movement");
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

    public void Rifle(AudioSource shooter_speaker)
    {
        shooter_speaker.volume = singleShotVolume;
        shooter_speaker.PlayOneShot(singleShot);
    }

    public void Shotgun(AudioSource shooter_speaker)
    {
        shooter_speaker.volume = shotgunVolume;
        shooter_speaker.PlayOneShot(shotgun);
    }

    public void Dash()
    {
        _speaker.PlayOneShot(dash);
    }
}
