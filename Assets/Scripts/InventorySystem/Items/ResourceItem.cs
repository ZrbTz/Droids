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
        return Activate();
    }

    public override void SetItemData(GameObject item)
    {
    }

    /* PLACEABLE */
    public bool Activate()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        LayerMask tmpIgnoreLayers = ~ignoreLayers;

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20f, tmpIgnoreLayers))
        {
            ResourceActivable target = hitInfo.collider.gameObject.GetComponent<ResourceActivable>();
            if (target != null)
            {
                return target.TryActivate(this);
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
}