using UnityEngine;

public abstract class Actionable : Interactable
{
    public override void Interact(GameObject player)
    {
        Destroy(gameObject);
    }
}
