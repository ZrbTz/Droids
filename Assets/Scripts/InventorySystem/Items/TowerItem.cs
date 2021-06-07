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
                Vector3 boxSize = prefab.GetComponent<BoxCollider>().size / 2;
                Vector3 boxCenter = prefab.GetComponent<BoxCollider>().center;
                int boxlayerMask = ~LayerMask.GetMask("AreaEffect", "Projectile", "Item", "Ground");
                Collider[] boxHit = Physics.OverlapBox(hitInfo.point + boxCenter, boxSize, Quaternion.identity, boxlayerMask, QueryTriggerInteraction.Ignore);
                Debug.Log(boxHit);
                if (boxHit.Length == 0) {
                    GameObject newPlaced = Instantiate(GetPlaceableItemPrefab());

                    newPlaced.transform.position = hitInfo.point;
                    newPlaced.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
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

}