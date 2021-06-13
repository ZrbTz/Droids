using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    //SOlo per testing
    [SerializeField] bool mute;
    [SerializeField] float maxDistance = 50.0f;

    [SerializeField] AudioClip walkStep;
    public bool engined;
    [SerializeField] [Range(0.0f, 1.0f)] float stepVolume;
    [SerializeField] AudioClip movement;
    [SerializeField] AudioClip attack;
    [SerializeField] [Range(0.0f, 1.0f)] float attackVolume;

    private AudioSource _speaker;
    private AudioSource attack_speaker;


    // Start is called before the first frame update
    void Start()
    {
        _speaker = CreateSpeaker(stepVolume);
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
        float scaleAnimationCurve = 500 / maxDistance;
        newSpeaker.maxDistance = 500/scaleAnimationCurve;
        newSpeaker.minDistance = 1/scaleAnimationCurve;

        newSpeaker.mute = mute;
        return newSpeaker;
    }

    public void Step()
    {
        _speaker.PlayOneShot(walkStep);
    }

    public void StartEngine()
    {
        Debug.Log("STARTENGINE");
        _speaker.clip = walkStep;
        _speaker.Play();
    }

    public void StopEngine()
    {
        _speaker.Stop();
    }

    public void Movement()
    {
        _speaker.PlayOneShot(movement);
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
