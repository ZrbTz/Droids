using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameUI gameUI;

    [SerializeField]
    private int size = 5;
    private InventorySlot[] inventory;
    [SerializeField]
    private int selectedSlot = 0;
    public int[] inventorySlotSize = { 1, 5 };

    private bool isShowingPreview = false;
    private GameObject previewItem;

    void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    void Start()
    {
        inventory = new InventorySlot[size];
    }

    void Update()
    {
        if (inventory[0] != null)
        {
            TowerItem towerItem = (TowerItem)inventory[0].item;
            if (!isShowingPreview)
            {
                SpawnPreview(towerItem);
            }
            else
            {
                MovePreview(towerItem);
            }
        }
    }

    private void SpawnPreview(TowerItem towerItem)
    {
        previewItem = (GameObject)Instantiate((towerItem.GetPlaceablePreviewItemPrefab()));
        previewItem.SetActive(false);
        isShowingPreview = true;
    }

    private void MovePreview(TowerItem towerItem)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        LayerMask tmpIgnoreLayers = ~towerItem.GetLayerMask();

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20f, tmpIgnoreLayers))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                GameObject prefab = towerItem.GetPlaceableItemPrefab();
                BoxCollider box = prefab.GetComponent<BoxCollider>();
                int boxlayerMask = ~LayerMask.GetMask("AreaEffect", "Projectile", "Item", "Ground");
                Collider[] boxHit = Physics.OverlapBox(hitInfo.point + box.center, box.size / 2, Quaternion.identity, boxlayerMask, QueryTriggerInteraction.Ignore);
                if (boxHit.Length == 0) {
                    previewItem.SetActive(true);
                    previewItem.transform.position = hitInfo.point;
                    previewItem.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                    return;
                }

            }
        }
        previewItem.SetActive(false);
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

            if (slot == 1)
            {
                gameUI.UpdateGrenadeAmount(item.GetAmount());
            }
            else if (slot == 0)
            {
                gameUI.AddTowerIcon(item.GetIconSprite());
            }

            return true;
        }
        if (inventory[slot].item == item)
        {
            if (inventory[slot].amount < inventorySlotSize[slot])
            {
                inventory[slot].addAmount(1);

                if (slot == 1)
                {
                    gameUI.UpdateGrenadeAmount(inventory[slot].amount);
                }

                return true;
            }
            return false;
        }

        return false;
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

            if (slot == 1)
            {
                gameUI.UpdateGrenadeAmount(amount);
            }
            else if (slot == 0)
            {
                gameUI.RemoveTowerIcon();
                isShowingPreview = false;
                Destroy(previewItem);
            }
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
