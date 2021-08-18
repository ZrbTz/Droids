using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        Click,
        Hold
    }

    [SerializeField]
    private InteractionType interactionType;

    [SerializeField]
    private float holdTime = 1.0f;
    private float currentHoldTime;

    protected bool isEnabled = true;

    public void IncreaseCurrentHoldTime()
    {
        currentHoldTime += Time.deltaTime;
    }
    public void ResetCurrentHoldTime()
    {
        currentHoldTime = 0f;
    }

    public abstract void Interact(GameObject player);

    public void EnableInteraction()
    {
        isEnabled = true;
    }

    public void DisableInteraction()
    {
        isEnabled = false;
    }


    /* GETTERS */
    public InteractionType GetInteractionType()
    {
        return interactionType;
    }

    public float GetCurrentHoldTime()
    {
        return currentHoldTime;
    }

    public float GetHoldTime()
    {
        return holdTime;
    }

    public virtual bool IsEnabled(GameObject player)
    {
        return isEnabled;
    }
}