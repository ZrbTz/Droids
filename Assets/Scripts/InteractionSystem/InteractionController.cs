using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private float interactionDistance;
    private Camera cam;

    private int layerMask;

    void Start()
    {
        cam = Camera.main;
        layerMask = ~LayerMask.GetMask("Player");
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        bool successfulHit = false;

        if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && interactable.IsEnabled())
            {
                HandleInteraction(interactable);
                successfulHit = true;
            }
        }

        if (!successfulHit)
        {
            //gameUI.HideClickButton();
            //gameUI.HideHoldingButton();
        }
    }

    void HandleInteraction(Interactable interactable)
    {
        KeyCode key = interactable.GetInteractionKey();
        Interactable.InteractionType interactionType = interactable.GetInteractionType();

        switch (interactionType)
        {
            case Interactable.InteractionType.Click:
                //gameUI.ShowClickButton(key.ToString());

                if (Input.GetKeyDown(key))
                {
                    interactable.Interact(this.gameObject);
                }
                break;
            case Interactable.InteractionType.Hold:
                //gameUI.ShowHoldingButton(key.ToString());

                if (Input.GetKey(key))
                {
                    interactable.IncreaseCurrentHoldTime();
                    if (interactable.GetCurrentHoldTime() > interactable.GetHoldTime())
                    {
                        interactable.ResetCurrentHoldTime();
                        interactable.Interact(this.gameObject);
                    }
                }
                else
                {
                    interactable.ResetCurrentHoldTime();
                }

                //gameUI.UpdateHoldingButton(interactable.GetCurrentHoldTime() / interactable.GetHoldTime());
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }
}
