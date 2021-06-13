using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    //SOlo per testing
    [SerializeField] bool mute;
    [SerializeField] float maxDistance = 30.0f;

    [SerializeField] AudioClip walkStep;
    public bool engined;
    [SerializeField] [Range(0.0f, 1.0f)] float stepVolume;
    [SerializeField] AudioClip movement;
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
        move_speaker = CreateSpeaker(attackVolume);
        attack_speaker = CreateSpeaker(attackVolume);

        if (engined)
        {
            StartEngine();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private AudioSource CreateSpeaker(float intensity)
    {
        AudioSource newSpeaker = this.gameObject.AddComponent<AudioSource>();
        newSpeaker.volume = intensity;
        newSpeaker.playOnAwake = false;
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
        Debug.Log("STARTENGINE");
       step_speaker.clip = walkStep;
        step_speaker.Play();
    }

    public void StopEngine()
    {
        step_speaker.Stop();
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
}
