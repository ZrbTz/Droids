using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCooldownUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image dashCooldownBar;

    public void SetDashCooldown(float cooldown)
    {
        StartCoroutine(AnimateDashCooldownBar(dashCooldownBar, 0f, 1f, cooldown));
    }

    IEnumerator AnimateDashCooldownBar(UnityEngine.UI.Image bar, float origin, float target, float duration)
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
