using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Inventory System/Items/Tower")]
public class ThrowableItem : ItemObject
{
    [Header("Placeable Settings")]
    [SerializeField]
    private GameObject throwableItem;
    /*[SerializeField]
    private GameObject placeablePreviewItem;*/
    [SerializeField]
    private LayerMask layerMask;

    private Transform startPoint;
    public float throwSpeed = 5.0f;

    public override bool Use(GameObject player)
    {
        return Throw(player);
    }

    public override void SetItemData(GameObject item)
    {
    }

    public bool Throw(GameObject player)
    {
        //ferma la coroutine che mostra la traiettoria
        Vector3 direction = Camera.main.transform.forward;
        startPoint = player.transform.Find("Direzione");

        GameObject newPlaced = Instantiate(GetPlaceableItemPrefab());

        newPlaced.transform.position = startPoint.position;
        newPlaced.GetComponent<Rigidbody>().velocity = direction * throwSpeed;

        return true;
    }

    public override bool ShowTrajectory(GameObject player)
    {
        //coroutine che mostra la traiettoria
        return true;
    }

    /* GETTERS */
    public GameObject GetPlaceableItemPrefab()
    {
        return throwableItem;
    }

    /* PLACEABLE */
    /*public bool Place()
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
    }*/

}