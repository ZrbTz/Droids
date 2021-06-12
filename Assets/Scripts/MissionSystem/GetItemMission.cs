using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GetItemMission", menuName = "Mission System/Get Item Mission")]
public class GetItemMission : Mission
{
    [SerializeField]
    private ItemObject item;
    private Inventory inventory;

    public override bool IsCompleted(GameObject player)
    {
        if (inventory == null)
        {
            inventory = player.GetComponent<Inventory>();
        }

        return inventory.CheckIfHasItem(item);
    }
}
