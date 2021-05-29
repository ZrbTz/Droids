using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject toDrop;

    public void Drop()
    {
        if(toDrop != null) Instantiate(toDrop, this.transform.position, Quaternion.identity);
    }
}
