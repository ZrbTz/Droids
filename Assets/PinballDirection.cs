using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballDirection : MonoBehaviour
{
    public GameObject percorso;

    private void OnTriggerExit(Collider other)
    {
        GameObject player = other.gameObject;
        if (player.layer == 10)
        {
            percorso.SetActive(false);
        }
    }
}
