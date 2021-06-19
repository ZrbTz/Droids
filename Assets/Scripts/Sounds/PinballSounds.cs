using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballSounds : MonoBehaviour
{
    private GameManager gameManager;
    private bool audioIsPaused = false;
    [SerializeField] [Range(0.0f, 1.0f)] float Volume = 0.1f;
    [SerializeField] float maxDistance = 30.0f;

    [SerializeField] AudioClip sound_effect;
    private AudioSource _speaker;

    // Start is called before the first frame update
    void Start()
    {
        _speaker = CreateSpeaker(Volume);
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.IsPaused() && !audioIsPaused)
        {
            audioIsPaused = true;
            _speaker.Pause();
        }
        else if (!gameManager.IsPaused() && audioIsPaused)
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
        newSpeaker.spatialBlend = 1.0f;
        newSpeaker.maxDistance = maxDistance;
        newSpeaker.rolloffMode = AudioRolloffMode.Linear;
        return newSpeaker;
    }

    public void PlaySound()
    {
        _speaker.PlayOneShot(sound_effect);
    }
}
