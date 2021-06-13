using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggestionsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject shownSuggestion;
    [SerializeField]
    private GameObject hiddenSuggestion;
    [SerializeField]
    private TMPro.TextMeshProUGUI title = default;
    [SerializeField]
    private TMPro.TextMeshProUGUI description = default;

    private bool isShown = true;

    public void SwitchWindow()
    {
        if (isShown)
        {
            HideSuggestion();
        }
        else
        {
            ShowSuggestion();
        }
    }

    public void ShowSuggestion()
    {
        shownSuggestion.SetActive(true);
        hiddenSuggestion.SetActive(false);

        isShown = true;
    }

    public void HideSuggestion()
    {
        shownSuggestion.SetActive(false);
        hiddenSuggestion.SetActive(true);

        isShown = false;
    }

    public void FullHideSuggestion()
    {
        shownSuggestion.SetActive(false);
        hiddenSuggestion.SetActive(false);

        isShown = false;
    }

    public void SetSuggestion(string title, string description)
    {
        this.title.SetText(title);
        this.description.SetText(description);
    }
}
