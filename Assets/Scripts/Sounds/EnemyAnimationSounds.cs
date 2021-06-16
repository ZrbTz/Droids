using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    private GameManager gameManager;
    private bool audioIsPaused = false;
    //SOlo per testing
    [SerializeField] bool mute;
    [SerializeField] [Range(0.0f, 1.0f)] float Volume = 0.1f;
    [SerializeField] float maxDistance = 30.0f;

    [SerializeField] AudioClip walkStep;
    public bool engined;
    private bool running = false;
    [SerializeField] AudioClip walkStop;
    [SerializeField] AudioClip movement;
    [SerializeField] AudioClip rotation;
    [SerializeField] AudioClip attack;

    private AudioSource _speaker;


    // Start is called before the first frame update
    void Start()
    {
        _speaker = CreateSpeaker(Volume);
        gameManager = FindObjectOfType<GameManager>();

        if (engined)
        {
            StartEngine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsPaused() && !audioIsPaused)
        {
            audioIsPaused = true;
            _speaker.Pause();
        }
        else if(!gameManager.IsPaused() && audioIsPaused)
        {
            audioIsPaused = false;
            _speaker.UnPause();
        }
    }

    private AudioSource CreateSpeaker(float intensity)
    {
        AudioSource newSpeaker = this.gameObject.AddComponent<AudioSource>();
        newSpeaker.volume = intensity;
        newSpeaker.playOnAwake = false;
        newSpeaker.loop = true;
        newSpeaker.spatialBlend = 1.0f;
        newSpeaker.maxDistance = maxDistance;
        newSpeaker.rolloffMode = AudioRolloffMode.Linear;

        newSpeaker.mute = mute;
        return newSpeaker;
    }

    public void Step()
    {
        _speaker.PlayOneShot(walkStep);
    }

    public void StartEngine()
    {
        if (engined)
        {
            Debug.Log("STARTENGINE");
            _speaker.clip = walkStep;
            _speaker.Play();
            running = true;
        }
    }

    public void StopEngine()
    {
        if (engined)
        {
            _speaker.Stop();
            Debug.Log("STOPENGINE");
            if (walkStop && running)
            {
                running = false;
                _speaker.PlayOneShot(walkStop);
            }
        }
    }

    public void Movement()
    {
       _speaker.PlayOneShot(movement);
    }

    public void Impact()
    {
        _speaker.PlayOneShot(attack);
    }
    #region Summoner
    public void OpeningSound()
    {
        Debug.Log("Opening");
        _speaker.PlayOneShot(movement);
    }

    public void ClosingSound()
    {
        Debug.Log("Closing");
        _speaker.PlayOneShot(attack);
    }
    #endregion

    #region Bomber
    public void Rotate()
    {
        _speaker.PlayOneShot(rotation);
    }
    #endregion
}
