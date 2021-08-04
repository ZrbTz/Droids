using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceActivable : Interactable
{
    public int risorsaRichiesta = -1;

    public override void Interact(GameObject player)
    {
        ItemObject it = player.GetComponent<Inventory>().GetSelectedItemObject(2);
        if (it != null)
        {
            TryActivate((ResourceItem) it);
        }
        
    }

    public bool TryActivate(ResourceItem risorsaUtilizzata)
    {
        if (risorsaUtilizzata.resourceType == risorsaRichiesta)
        {
            Debug.Log("Funziona!");
            Activate();
            return true;
        }
        return false;
    }

    public virtual void Activate()
    {
        //in base all'oggetto (classe ereditaria) comportamento diverso
    }
}
