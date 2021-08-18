using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Inventory System/Items/Resource")]
public class ResourceItem : ItemObject
{
    [Header("Placeable Settings")]
    [SerializeField]
    private GameObject resourceItem;
    [SerializeField]
    private LayerMask ignoreLayers;
    private float health;
    public int resourceType = -1; //sostituire con un enum?

    public override bool Use(GameObject player)
    {
        return Activate(player);
    }

    public override void SetItemData(GameObject item)
    {
    }

    /* PLACEABLE */
    public bool Activate(GameObject player)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        LayerMask tmpIgnoreLayers = ~ignoreLayers;

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20f, tmpIgnoreLayers))
        {
            ResourceActivable target = hitInfo.collider.gameObject.GetComponent<ResourceActivable>();
            if (target != null)
            {
                if(target.IsEnabled(player))
                {
                    target.Activate();
                    return true;
                }
                 
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    public LayerMask GetLayerMask()
    {
        return ignoreLayers;
    }

    public GameObject Oggetto()
    {
        return resourceItem;
    }
}