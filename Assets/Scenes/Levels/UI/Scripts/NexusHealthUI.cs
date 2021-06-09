using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusHealthUI : MonoBehaviour
{
    [Header("Nexus Health Settings")]
    [SerializeField]
    private float barAnimationDuration;
    [SerializeField]
    private UnityEngine.UI.Image nexusHealthBar;

    [Header("Horde Number Settings")]
    [SerializeField]
    private TMPro.TextMeshProUGUI hordeNumber;

    public void SetHordeNumber(int horde)
    {
        hordeNumber.SetText(horde.ToString());
    }

    public void SetNexusHealth(float percentage)
    {
        StartCoroutine(AnimateNexusHealthBar(nexusHealthBar, nexusHealthBar.fillAmount, percentage, barAnimationDuration));
    }

    IEnumerator AnimateNexusHealthBar(UnityEngine.UI.Image bar, float origin, float target, float duration)
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
