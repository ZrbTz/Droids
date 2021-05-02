using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNexus : MonoBehaviour
{
    public Unit nexus;
    public GameObject test;

    private void Awake()
    {
        nexus = FindObjectOfType<GameManager>().nexus;
    }

    private void OnTriggerEnter(Collider other)
    {
        test = other.gameObject;
        if (other.GetComponent<Enemy>() != null)
        {
            nexus.health -= other.GetComponent<Enemy>().damage;
            other.GetComponent<Enemy>().FadeAndDisappear();
            Debug.Log("Il nexus ha subito danno!");
        }
    }
}
