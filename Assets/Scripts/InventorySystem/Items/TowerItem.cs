using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Inventory System/Items/Tower")]
public class TowerItem : ItemObject
{
    [Header("Placeable Settings")]
    [SerializeField]
    private GameObject placeableItem;
    [SerializeField]
    private GameObject placeablePreviewItem;
    [SerializeField]
    private LayerMask ignoreLayers;
    private float health;

    public override bool Use(GameObject player)
    {
        return Place();
    }

    public override void SetItemData(GameObject item)
    {
    }

    /* PLACEABLE */
    public bool Place()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        LayerMask tmpIgnoreLayers = ~ignoreLayers;

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20f, tmpIgnoreLayers))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                GameObject prefab = GetPlaceableItemPrefab();
                BoxCollider box = prefab.GetComponent<BoxCollider>();
                int boxlayerMask = ~LayerMask.GetMask("AreaEffect", "Projectile", "Item", "Ground");
                Collider[] boxHit = Physics.OverlapBox(hitInfo.point + box.center, box.size/2, Quaternion.identity, boxlayerMask, QueryTriggerInteraction.Ignore);
                if (boxHit.Length == 0) {
                    GameObject newPlaced = Instantiate(prefab);

                    newPlaced.transform.position = hitInfo.point;
                    newPlaced.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                    Obstacle o = newPlaced.GetComponent<Obstacle>();
                    if(o != null) newPlaced.GetComponent<Obstacle>().health = health;
                    return true;
                }
                else return false;

                
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    /* GETTERS */
    public GameObject GetPlaceableItemPrefab()
    {
        return placeableItem;
    }
    public GameObject GetPlaceablePreviewItemPrefab()
    {
        return placeablePreviewItem;
    }

    public LayerMask GetLayerMask()
    {
        return ignoreLayers;
    }

    public override void setHealth(float newHealth) {
        health = newHealth;
    }
}