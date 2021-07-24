using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour {
    public float size = 3.0f;
    private void OnTriggerEnter(Collider other) {
        Enemy enemy = other.transform.root.GetComponent<Enemy>();
        if(enemy != null) {
            //Debug.Log("ASD");
            enemy.updatePath(this.gameObject);
        }
    }
}
