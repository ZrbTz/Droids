using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSounds : MonoBehaviour
{
    [SerializeField] AudioClip walkStep;
    [SerializeField] AudioClip walkMovement;
    [SerializeField] [Range(0.0f, 1.0f)] float stepVolume = 0.04f;
    [SerializeField] AudioClip attack;
    [SerializeField] [Range(0.0f, 1.0f)] float attackVolume = 0.09f;

    private AudioSource _speaker;
    private AudioSource attack_speaker;

    // Start is called before the first frame update
    void Start()
    {
        _speaker = CreateSpeaker(stepVolume);
        attack_speaker = CreateSpeaker(attackVolume);
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
        return newSpeaker;
    }

    public void Step()
    {
        _speaker.PlayOneShot(walkStep);
    }

    public void Movement()
    {
        _speaker.PlayOneShot(walkMovement);
    }

    public void Impact()
    {
        attack_speaker.PlayOneShot(attack);
    }
}
