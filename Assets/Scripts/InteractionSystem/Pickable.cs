using UnityEngine;

public class Pickable : Interactable
{
    public int inventorySlot = -1; //0 per torri, 1 per altro
    [SerializeField]
    private ItemObject itemObject;
    [SerializeField]
    private AudioClip pickSound;

    public override void Interact(GameObject player)
    {
        //player.GetComponent<AudioSource>().PlayOneShot(pickSound, 0.5f);
        if(player.GetComponent<Inventory>().AddItem(itemObject, inventorySlot))
        {
            Destroy(gameObject);
        }
        
    }
}
