using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour {
    public Portal next;
    [HideInInspector] public float time = float.NegativeInfinity;
    public float radius;

    private List<GameObject> towers = new List<GameObject>();

    public void Activate() {
        List<GameObject> copy = GetTowers();
        Portal previous = this;
        Portal portal = next;
        do {
            List<GameObject> temp = portal.GetTowers();
            portal.SetTowers(copy, previous.transform.position);
            copy = temp;
            previous = portal;
            portal = portal.next;
            portal.time = Time.time;
        } while (previous != this);
    }

    private List<GameObject> GetTowers() {
        var towers = Physics.OverlapSphere(transform.position, radius).Where(collider => collider.CompareTag("Tower"))
            .Select(collider => collider.gameObject).Distinct().ToList();
        //towers = towers.Where(tower => tower != null).ToList();
        return towers;
    }

    private void SetTowers(List<GameObject> towers, Vector3 basePosition) {
        foreach(GameObject tower in towers) {
            Vector3 offset = tower.transform.position - basePosition;
            tower.transform.position = transform.position + offset;
        }
    }

    /*private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Tower")) {
            towers.Add(other.gameObject);
            
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Tower"))
            towers.Remove(other.gameObject);
    }*/

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
