using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInfosUI : MonoBehaviour
{
    [SerializeField]
    private GameObject holdingButton = default;
    [SerializeField]
    private UnityEngine.UI.Image holdingBackground = default;
    [SerializeField]
    private TMPro.TextMeshProUGUI holdingText = default;

    [SerializeField]
    private GameObject clickButton = default;
    [SerializeField]
    private TMPro.TextMeshProUGUI clickText = default;

    public void ShowHoldingButton(string key)
    {
        holdingButton.SetActive(true);
        holdingText.text = key;
    }

    public void UpdateHoldingButton(float value)
    {
        holdingBackground.fillAmount = value;
    }

    public void HideHoldingButton()
    {
        holdingButton.SetActive(false);
    }

    public void ShowClickButton(string key)
    {
        clickButton.SetActive(true);
        clickText.text = key;
    }

    public void HideClickButton()
    {
        clickButton.SetActive(false);
    }
}
