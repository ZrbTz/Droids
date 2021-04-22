using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public Unit target;
    public float speed = 10f;
    public float damage;

    void Update() {
        if(target.dead) {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = target.transform.position - transform.position; direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Unit>() == target) {
            target.health -= damage;
            Destroy(gameObject);
        }
    }
}
