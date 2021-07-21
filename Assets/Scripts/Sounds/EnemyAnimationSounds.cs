using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    private GameManager gameManager;
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
        gameManager = GameManager.Instance;
        gameManager.pauseEvent.AddListener(pauseAudio);
        gameManager.resumeEvent.AddListener(unpauseAudio);

        if (engined)
        {
            StartEngine();
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (gameManager.IsPaused() && !audioIsPaused)
    //    {
    //        audioIsPaused = true;
    //        _speaker.Pause();
    //    }
    //    else if(!gameManager.IsPaused() && audioIsPaused)
    //    {
    //        audioIsPaused = false;
    //        _speaker.UnPause();
    //    }
    //}

    void pauseAudio() {
        _speaker.Pause();
    }

    void unpauseAudio() {
        _speaker.UnPause();
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

    private void OnDestroy() {
        gameManager.pauseEvent.RemoveListener(pauseAudio);
        gameManager.resumeEvent.RemoveListener(unpauseAudio);
    }
    #region Summoner
    public void OpeningSound()
    {
        _speaker.PlayOneShot(movement);
    }

    public void ClosingSound()
    {
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
