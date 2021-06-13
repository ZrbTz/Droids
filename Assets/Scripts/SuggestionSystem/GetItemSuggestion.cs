using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GetItemSuggestion", menuName = "Suggestion System/Get Item Suggestion")]
public class GetItemSuggestion : Suggestion
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
