using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image rifleIcon;
    [SerializeField]
    private UnityEngine.UI.Image shotgunIcon;

    public void ShowRifleIcon() {
        rifleIcon.gameObject.SetActive(true);
        shotgunIcon.gameObject.SetActive(false);
    }

    public void ShowShotgunIcon() {
        rifleIcon.gameObject.SetActive(false);
        shotgunIcon.gameObject.SetActive(true);
    }
}
