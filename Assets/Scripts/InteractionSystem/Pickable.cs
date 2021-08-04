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
        Obstacle o = this.gameObject.GetComponent<Obstacle>();
        if (o != null) itemObject.setHealth(o.health); //se vita == 0 non dovrebbe essere cancellato? Se no un oggetto nell'inventario appena piazzato "muore"
        if(player.GetComponent<Inventory>().AddItem(itemObject, inventorySlot))
        {
            Destroy(gameObject);
        }
        
    }

    public ItemObject getItemObject() {
        return itemObject;
    }
}
