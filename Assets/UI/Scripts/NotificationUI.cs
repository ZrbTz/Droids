using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI notification;
    [SerializeField]
    private float fadeDuration;
    [SerializeField]
    private float standDuration;

    private bool isShowingNotification = false;

    public void SetNotification(string text)
    {
        notification.SetText(text);

        if (!isShowingNotification)
        {
            StartCoroutine(ShowNotification());
        }
        isShowingNotification = true;
    }

    IEnumerator ShowNotification()
    {
        yield return StartCoroutine(FadeNotification(notification, 0f, 1f, fadeDuration));
        yield return new WaitForSeconds(standDuration);
        yield return StartCoroutine(FadeNotification(notification, 1f, 0f, fadeDuration));

        isShowingNotification = false;
    }

    IEnumerator FadeNotification(TMPro.TextMeshProUGUI text, float origin, float target, float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            text.alpha = Mathf.Lerp(origin, target, percent);

            yield return null;
        }
    }
}
