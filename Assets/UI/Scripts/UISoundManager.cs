using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip hoverButton;
    [SerializeField]
    private AudioClip clickButton;
    [SerializeField]
    private float clickVolume = 0.7f;
    [SerializeField]
    private float hoverVolume = 0.5f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayClick()
    {
        PlayAudio(clickButton, clickVolume);
    }

    public void PlayHover()
    {
        PlayAudio(hoverButton, hoverVolume);
    }
}
