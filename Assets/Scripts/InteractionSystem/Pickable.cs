using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField]
    private AudioClip pickSound;

    public override void Interact(GameObject player)
    {
        Destroy(gameObject);
    }
}
