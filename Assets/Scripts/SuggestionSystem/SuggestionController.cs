using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggestionController : MonoBehaviour
{
    [Header("Suggestions Settings")]
    [SerializeField]
    private Suggestion[] suggestions;
    private Suggestion currentSuggestion;
    private int numSuggestions;
    private int currentSuggestionIndex;
    private bool isActive = true;

    private GameObject player;
    private GameUI gameUI;
    public Dictionary<string, DoActionSuggestion> actions;

    private static SuggestionController instance;
    public static SuggestionController Instance { get => instance; }

    void Awake()
    {
        actions = new Dictionary<string, DoActionSuggestion>();
        instance = this;
        player = FindObjectOfType<InputManager>().gameObject;
        gameUI = FindObjectOfType<GameUI>();
    }

    void Start()
    {
        numSuggestions = suggestions.Length;
        if (numSuggestions > 0)
        {
            currentSuggestionIndex = 0;
            StartSuggestion(0);
        }
    }


    void Update()
    {
        if (isActive)
        {
            if (currentSuggestion.IsCompleted(player))
            {
                currentSuggestionIndex++;
                if (currentSuggestionIndex < numSuggestions)
                {
                    StartSuggestion(currentSuggestionIndex);
                }
                else
                {
                    isActive = false;
                    gameUI.FullHideSuggestion();
                }
            }
        }

        // TODO: go to input manager
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            gameUI.SwitchSuggestionWindow();
        }
    }

    private void StartSuggestion(int index)
    {
        currentSuggestion = suggestions[currentSuggestionIndex];
        currentSuggestion.init();
        UpdateUI(suggestions[currentSuggestionIndex]);
    }

    private void UpdateUI(Suggestion suggestion)
    {
        if (currentSuggestion.isVisible()) {
            gameUI.UpdateSuggestion(suggestion.GetTitle(), suggestion.GetDescription());
            gameUI.ShowSuggestion();
        }
        else gameUI.FullHideSuggestion();
    }

    public bool isBlocking() {
        return isActive && currentSuggestion.isBlocking();
    }
}
