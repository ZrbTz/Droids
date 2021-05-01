using UnityEngine;

public class ProximityPickable : Interactable
{
    public int inventorySlot = -1; //0 per torri, 1 per altro
    [SerializeField]
    private ItemObject itemObject;
    [SerializeField]
    private AudioClip pickSound;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ThirdPersonControllerDash>() != null)
        {
            Interact(other.gameObject);
        }
    }

    public override void Interact(GameObject player)
    {
        //player.GetComponent<AudioSource>().PlayOneShot(pickSound, 0.5f);
        if(IsEnabled())
        {
            if (player.GetComponent<Inventory>().AddItem(itemObject, inventorySlot))
            {
                Destroy(gameObject);
            }
        }
    }
}