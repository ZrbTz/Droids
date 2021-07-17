using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPauseController : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource _speaker;

    // Start is called before the first frame update
    void Start()
    {
        _speaker = this.GetComponent<AudioSource>();
        gameManager = GameManager.Instance;
        gameManager.pauseEvent.AddListener(pauseAudio);
        gameManager.resumeEvent.AddListener(unpauseAudio);
    }

    void pauseAudio() {
        _speaker.Pause();
    }

    void unpauseAudio() {
        _speaker.UnPause();
    }

    //// Update is called once per frame
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

    private void OnDestroy() {
        gameManager.pauseEvent.RemoveListener(pauseAudio);
        gameManager.resumeEvent.RemoveListener(unpauseAudio);
    }
}
