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
        if (other.GetType() == typeof(CapsuleCollider) && other.transform.root.GetComponent<Enemy>() != null) {
            StartCoroutine(damageNexus(other));
        }
    }

    IEnumerator damageNexus(Collider other) {
        yield return new WaitForSeconds(1);
        if (other.transform.root.GetComponent<Enemy>().dead == false) {
            other.gameObject.GetComponent<DropItem>().Drop();
            nexus.health -= other.transform.root.GetComponent<Enemy>().damage;
            other.transform.root.GetComponent<Enemy>().FadeAndDisappear();
            Debug.Log("Il nexus ha subito danno!");
        }
    }
}
