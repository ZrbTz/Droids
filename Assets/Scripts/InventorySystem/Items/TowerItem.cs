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
    private LayerMask layerMask;

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

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 20f, layerMask))
        {
            GameObject newPlaced = Instantiate(GetPlaceableItemPrefab());

            newPlaced.transform.position = hitInfo.point;
            newPlaced.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            return true;
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