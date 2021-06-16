using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    private GameManager gameManager;
    private bool audioIsPaused = false;
    //SOlo per testing
    [SerializeField] bool mute;
    [SerializeField] float maxDistance = 30.0f;

    [SerializeField] AudioClip walkStep;
    public bool engined;
    private bool running = false;
    [SerializeField] AudioClip walkStop;
    [SerializeField] [Range(0.0f, 1.0f)] float stepVolume;
    [SerializeField] AudioClip movement;
    [SerializeField] AudioClip rotation;
    [SerializeField] [Range(0.0f, 1.0f)] float moveVolume;
    [SerializeField] AudioClip attack;
    [SerializeField] [Range(0.0f, 1.0f)] float attackVolume;

    private AudioSource step_speaker;
    private AudioSource move_speaker;
    private AudioSource attack_speaker;


    // Start is called before the first frame update
    void Start()
    {
        step_speaker = CreateSpeaker(stepVolume);
        move_speaker = CreateSpeaker(moveVolume);
        attack_speaker = CreateSpeaker(attackVolume);
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
            step_speaker.Pause();
            move_speaker.Pause();
            attack_speaker.Pause();
        }
        else if(!gameManager.IsPaused() && audioIsPaused)
        {
            audioIsPaused = false;
            step_speaker.UnPause();
            move_speaker.UnPause();
            attack_speaker.UnPause();
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
        step_speaker.PlayOneShot(walkStep);
    }

    public void StartEngine()
    {
        if (engined)
        {
            Debug.Log("STARTENGINE");
            step_speaker.clip = walkStep;
            step_speaker.Play();
            running = true;
        }
    }

    public void StopEngine()
    {
        if (engined)
        {
            step_speaker.Stop();
            Debug.Log("STOPENGINE");
            if (walkStop && running)
            {
                running = false;
                step_speaker.PlayOneShot(walkStop);
            }
        }
    }

    public void Movement()
    {
       move_speaker.PlayOneShot(movement);
    }

    public void Impact()
    {
        attack_speaker.PlayOneShot(attack);
    }
    #region Summoner
    public void OpeningSound()
    {
        Debug.Log("Opening");
        attack_speaker.PlayOneShot(movement);
    }

    public void ClosingSound()
    {
        Debug.Log("Closing");
        attack_speaker.PlayOneShot(attack);
    }
    #endregion

    #region Bomber
    public void Rotate()
    {
        move_speaker.PlayOneShot(rotation);
    }
    #endregion
}
