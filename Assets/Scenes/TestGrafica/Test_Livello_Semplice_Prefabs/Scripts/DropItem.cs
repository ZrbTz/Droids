using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject toDrop;

    private void OnDestroy()
    {
        Instantiate(toDrop, this.transform.position, Quaternion.identity);
    }
}
