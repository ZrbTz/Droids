using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Tower,
    Throwable
}

[System.Serializable]
public abstract class ItemObject : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private Sprite iconSprite;
    [SerializeField]
    private int amount = 1;

    public virtual bool Use(GameObject player)
    {
        return false;
    }

    public virtual bool ShowTrajectory(GameObject player)
    {
        return false;
    }

    public virtual void SetItemData(GameObject item)
    {
        //item.GetComponent<ItemController<ItemObject>>().SetItemData(this); ;
    }

    /* GETTERS */
    public ItemType GetItemType()
    {
        return itemType;
    }

    public int GetAmount()
    {
        return amount;
    }

    public Sprite GetIconSprite()
    {
        return iconSprite;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public virtual void setHealth(float newHealth) { return; }
}