using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : Interactable {
    public List<ShopItemData> shopItems;
    public Portal portal;
    public Transform turretSpawnTransform;

    public override void Interact(GameObject player) {
        if (CheckSpawnArea()) {
            FindObjectOfType<GameUI>().ShowShop(this);
        } else {
            FindObjectOfType<GameUI>().UpdateNotification("Remove any turrets in the building area");
        }
    }

    public bool CheckSpawnArea() {
        return portal.GetTowers().Count == 0;
    }

    public void SpawnTurret(ShopItemData shopItem) {
        var turret = Instantiate(shopItem.prefab);
        turret.transform.SetPositionAndRotation(turretSpawnTransform.position, turretSpawnTransform.rotation);
    }
}
