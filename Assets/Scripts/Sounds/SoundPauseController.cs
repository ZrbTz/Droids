using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPauseController : MonoBehaviour
{
    private GameManager gameManager;
    private bool audioIsPaused = false;
    private AudioSource _speaker;

    // Start is called before the first frame update
    void Start()
    {
        _speaker = this.GetComponent<AudioSource>();
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
        else if(!gameManager.IsPaused() && audioIsPaused)
        {
            audioIsPaused = false;
            _speaker.UnPause();
        }
    }
}
