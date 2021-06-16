using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class PlayerAnimationSounds : MonoBehaviour
{
    private GameManager gameManager;
    private bool audioIsPaused = false;
    [SerializeField] [Range(0.0f, 1.0f)] float Volume = 0.1f;
    [SerializeField] float maxDistance = 30.0f;
    [SerializeField] GameObject shootingPoint;

    [SerializeField] AudioClip walkStep;
    [SerializeField] AudioClip walkMovement;
    [SerializeField] AudioClip jumpStart;
    [SerializeField] AudioClip singleShot;
    [SerializeField] AudioClip shotgun;
    [SerializeField] AudioClip dash;

    private AudioSource _speaker;
    private AudioSource gun_speaker;
    private vThirdPersonController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _speaker = CreateSpeaker(Volume, this.gameObject);
        gun_speaker = CreateSpeaker(Volume, shootingPoint);
        _controller = this.GetComponent<vThirdPersonController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.IsPaused() && !audioIsPaused)
        {
            _speaker.Pause();
            gun_speaker.Pause();
        }
        else if (!gameManager.IsPaused() && audioIsPaused)
        {
            _speaker.UnPause();
            gun_speaker.UnPause();
        }
    }

    private AudioSource CreateSpeaker(float intensity, GameObject origin)
    {
        AudioSource newSpeaker = origin.AddComponent<AudioSource>();
        newSpeaker.volume = intensity;
        newSpeaker.playOnAwake = false;
        newSpeaker.spatialBlend = 1.0f;
        newSpeaker.maxDistance = maxDistance;
        newSpeaker.rolloffMode = AudioRolloffMode.Linear;
        return newSpeaker;
    }

    #region Step
    public void StepForward()
    {
        if (_controller.input.z > 0.01f && Mathf.Abs(_controller.input.x) < 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepForwardSx()
    {
        if (_controller.input.z > 0.01f && _controller.input.x <-0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepForwardDx()
    {
        if (_controller.input.z > 0.01f && _controller.input.x > 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepSx()
    {
        if (Mathf.Abs(_controller.input.z) < 0.01f && _controller.input.x < -0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepDx()
    {
        if (Mathf.Abs(_controller.input.z) < 0.01f && _controller.input.x > 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackwardSx()
    {
        if (_controller.input.z < -0.01f && _controller.input.x > 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackwardDx()
    {
        if (_controller.input.z < -0.01f && _controller.input.x < -0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void StepBackward()
    {
        if (_controller.input.z < -0.01f && Mathf.Abs(_controller.input.x) < 0.01f && _controller.isJumping == false && _controller.isSprinting == false)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    #endregion
    public void Run()
    {
        if (_controller.inputMagnitude > 0.01f && _controller.isJumping==false && _controller.isSprinting == true)
        {
            _speaker.PlayOneShot(walkStep);

        }
    }
    public void Movement()
    {
        if (_controller.inputMagnitude > 0.01f)
        {
            _speaker.PlayOneShot(walkMovement);

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

    public void Rifle()
    {
        gun_speaker.PlayOneShot(singleShot);
    }

    public void Shotgun()
    {
        gun_speaker.PlayOneShot(shotgun);
    }

    public void Dash()
    {
        _speaker.PlayOneShot(dash);
    }
}
