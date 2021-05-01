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
    public int[] inventorySlotSize = { 1, 5 };

    void Start()
    {
        inventory = new InventorySlot[size];
    }

    public bool AddItem(ItemObject item, int slot)
    {
        if (slot == -1)
        {
            return false;
        }
        if (inventory[slot] == null)
        {
            inventory[slot] = new InventorySlot(item, item.GetAmount());
            return true;
        }
        if (inventory[slot].item == item)
        {
            if (inventory[slot].amount < inventorySlotSize[slot])
            {
                inventory[slot].addAmount(1);
                return true;
            }
            return false;
        }
        return false;
        /*
        for (int i = 0; i < size; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = new InventorySlot(item, item.GetAmount());
                //gameUI.AddItem(item.GetIconPrefab(), item.GetAmount(), i);
                break;
            }
        }
        */
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

    public bool ShowThrowableTrajectory(int slot)
    {
        if (inventory[slot] != null)
        {
            inventory[slot].item.ShowTrajectory(this.gameObject);
            return true;
        }
        return false;
    }

    public bool UseItem(int slot)
    {
        if (inventory[slot] != null)
        {
            bool used = inventory[slot].item.Use(this.gameObject);
            if (used)
            {
                DecreaseItemAmount(slot);
            }
            return true;
        }
        return false;
    }

    public void DecreaseItemAmount(int slot)
    {
        int amount = inventory[slot].addAmount(-1);
        if (amount <= 0)
        {
            inventory[slot] = null;
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
