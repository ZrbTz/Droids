using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCooldownUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image dashCooldownBar;
    [SerializeField]
    private TMPro.TextMeshProUGUI dashNumber;

    public void SetDashCooldown(float cooldown)
    {
        StartCoroutine(AnimateDashCooldownBar(dashCooldownBar, 0f, 1f, cooldown));
    }

    public void SetDashNumber(int number)
    {
        dashNumber.text = "x" + number.ToString();
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
