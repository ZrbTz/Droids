using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField]
    private ItemObject itemObject;
    [SerializeField]
    private AudioClip pickSound;

    public override void Interact(GameObject player)
    {
        //player.GetComponent<AudioSource>().PlayOneShot(pickSound, 0.5f);
        player.GetComponent<Inventory>().AddItem(itemObject);
        Destroy(gameObject);
    }
}
