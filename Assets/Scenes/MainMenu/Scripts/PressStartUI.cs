using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStartUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private UISoundManager soundManager;
    [SerializeField]
    private AudioClip pressAnyKeyClip;
    [SerializeField]
    private float volume = 0.7f;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
            soundManager.PlayAudio(pressAnyKeyClip, volume);
        }
    }
}
