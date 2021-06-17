using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNexus : MonoBehaviour
{
    public Unit nexus;
    public GameObject test;
    private GameUI gameUI;

    private void Awake()
    {
        nexus = FindObjectOfType<GameManager>().nexus;
        gameUI = FindObjectOfType<GameUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy e = other.transform.root.GetComponent<Enemy>();
        if (e != null && !e.hitNexus) {
            e.hitNexus = true;
            StartCoroutine(damageNexus(other));
        }
    }

    IEnumerator damageNexus(Collider other) {
        yield return new WaitForSeconds(1);
        if (other != null && other.transform.root.GetComponent<Enemy>().dead == false) {
            DropItem dropItem = other.transform.root.GetComponent<DropItem>();
            if (dropItem != null) dropItem.Drop();
            nexus.health -= other.transform.root.GetComponent<Enemy>().nexusDamage;
            other.transform.root.GetComponent<Enemy>().FadeAndDisappear();
            other.transform.root.gameObject.SetActive(false);
            Debug.Log("Il nexus ha subito danno!");

            gameUI.UpdateNexusHealth(nexus.health, nexus.GetMaxHealth());
        }
    }
}
