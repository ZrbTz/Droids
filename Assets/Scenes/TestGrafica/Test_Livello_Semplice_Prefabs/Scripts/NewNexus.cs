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
        if (other.GetComponent<Enemy>() != null)
        {
            StartCoroutine(damageNexus(other));
        }
    }

    IEnumerator damageNexus(Collider other) {
        yield return new WaitForSeconds(1);
        if (other.GetComponent<Enemy>().dead == false) {
            nexus.health -= other.GetComponent<Enemy>().damage;
            other.GetComponent<Enemy>().FadeAndDisappear();
            Debug.Log("Il nexus ha subito danno!");
        }
    }
}
