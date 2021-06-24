using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour { 
    private void OnTriggerEnter(Collider other) {
        Enemy enemy = other.transform.root.GetComponent<Enemy>();
        if(enemy != null) {
            //Debug.Log("ASD");
            enemy.updatePath(this.gameObject);
        }
    }
}
