using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypeUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image rifleIcon;
    [SerializeField]
    private UnityEngine.UI.Image shotgunIcon;
    [SerializeField]
    private UnityEngine.UI.Image shotgunLoadingIcon;

    public void ShowRifleIcon()
    {
        rifleIcon.gameObject.SetActive(true);
        shotgunIcon.gameObject.SetActive(false);
    }

    public void ShowShotgunIcon()
    {
        rifleIcon.gameObject.SetActive(false);
        shotgunIcon.gameObject.SetActive(true);
    }

    public void SetShotgunCooldown(float cooldown)
    {
        StartCoroutine(AnimateShotgunCooldownBar(shotgunLoadingIcon, 0f, 1f, cooldown));
    }

    IEnumerator AnimateShotgunCooldownBar(UnityEngine.UI.Image bar, float origin, float target, float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            bar.fillAmount = Mathf.Lerp(origin, target, percent);

            yield return null;
        }
    }
}
