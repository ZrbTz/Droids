using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = ~LayerMask.GetMask("Player", "AreaEffect", "Projectile", "Item");
    }

    // Update is called once per frame
    void Update(){
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore)) {
            transform.LookAt(hit.point);
        }
        //else  transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}