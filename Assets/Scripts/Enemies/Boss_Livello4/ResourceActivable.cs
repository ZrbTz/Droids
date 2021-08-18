using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceActivable : Interactable
{
    public int risorsaRichiesta = -1;

    public override void Interact(GameObject player)
    {
        Inventory inv = player.GetComponent<Inventory>();
        ItemObject it = inv.GetSelectedItemObject(2);
        if (it != null)
        {
            if(TryActivate((ResourceItem) it))
            {
                inv.DecreaseItemAmount(2);
            }
        }
        
    }

    bool CanActivate(ResourceItem risorsaUtilizzata)
    {
        return risorsaUtilizzata != null && risorsaUtilizzata.resourceType == risorsaRichiesta;
    }

    public bool TryActivate(ResourceItem risorsaUtilizzata)
    {
        if (CanActivate(risorsaUtilizzata))
        {
            Activate();
            return true;
        }
        return false;
    }

    public virtual void Activate()
    {
        //in base all'oggetto (classe ereditaria) comportamento diverso
    }

    public override bool IsEnabled(GameObject player)
    {
        return CanActivate((ResourceItem)player.GetComponent<Inventory>().GetSelectedItemObject(2));
    }
}
