using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour {
    public float size = 3.0f;
    public bool no_stop = false;
    public float stopping_time = 10.0f;
    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.tag);
        if (other.CompareTag("Bullet"))
        {
            //Debug.Log("Proiettile");
            return;
        }
        Enemy enemy = other.transform.root.GetComponent<Enemy>();
        if(enemy != null) {
            //Debug.Log("ASD");
            enemy.updatePath(this.gameObject);
        }
    }
}
