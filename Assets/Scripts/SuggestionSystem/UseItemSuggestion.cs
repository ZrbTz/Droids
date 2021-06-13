using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GetItemSuggestion", menuName = "Suggestion System/Use Item Suggestion")]
public class UseItemSuggestion : Suggestion
{
    [SerializeField]
    private ItemObject item;
    private Inventory inventory;
    private int previousItemAmount;

    public override bool IsCompleted(GameObject player)
    {
        if (inventory == null)
        {
            inventory = player.GetComponent<Inventory>();
            previousItemAmount = inventory.GetItemAmount(item);
        }

        int currentItemAmount = inventory.GetItemAmount(item);
        if (currentItemAmount < previousItemAmount)
        {
            return true;
        }

        previousItemAmount = currentItemAmount;
        return false;
    }
}
