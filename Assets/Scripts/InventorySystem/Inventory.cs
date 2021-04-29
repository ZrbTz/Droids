using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //[SerializeField]
    //private GameUI gameUI = default;

    [SerializeField]
    private int size = 5;
    private InventorySlot[] inventory;
    [SerializeField]
    private int selectedSlot = 0;

    void Start()
    {
        inventory = new InventorySlot[size];
    }

    public void AddItem(ItemObject item)
    {
        for (int i = 0; i < size; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = new InventorySlot(item, item.GetAmount());
                //gameUI.AddItem(item.GetIconPrefab(), item.GetAmount(), i);
                break;
            }
        }
    }

    public void SelectSlot(int newPosition)
    {
        //gameUI.UpdateSelectedSlot(newPosition, selectedSlot);
        selectedSlot = newPosition;
    }

    public void SelectNextSlot()
    {
        int newPosition = (selectedSlot + 1) % 5;
        //gameUI.UpdateSelectedSlot(newPosition, selectedSlot);
        selectedSlot = newPosition;
    }

    public void SelectPreviousSlot()
    {
        int newPosition = (selectedSlot - 1 + 5) % 5;
        //gameUI.UpdateSelectedSlot(newPosition, selectedSlot);
        selectedSlot = newPosition;
    }

    public void UseSelectedItem()
    {
        if (inventory[selectedSlot] != null)
        {
            bool used = inventory[selectedSlot].item.Use(this.gameObject);
            if (used)
            {
                DecreaseSelectedItemAmount();
            }
        }
    }

    public void DecreaseSelectedItemAmount()
    {
        int amount = inventory[selectedSlot].addAmount(-1);
        if (amount <= 0)
        {
            inventory[selectedSlot] = null;
            //gameUI.RemoveItem(selectedSlot);
        }
        else
        {
            //gameUI.UpdateItemCounter(amount, selectedSlot);
        }
    }

    public ItemObject GetSelectedItemObject()
    {
        if (inventory[selectedSlot] != null)
        {
            return inventory[selectedSlot].item;
        }

        return null;
    }

    public bool CheckIfHasItem(ItemObject item)
    {
        for (int i = 0; i < size; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].item == item)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public int GetItemAmount(ItemObject item)
    {
        for (int i = 0; i < size; i++)
        {
            if (inventory[i] != null)
            {
                if (inventory[i].item == item)
                {
                    return inventory[i].amount;
                }
            }
        }

        return 0;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public int addAmount(int value)
    {
        this.amount += value;
        return this.amount;
    }

    public int GetTotalAmount()
    {
        return amount;
    }
}
