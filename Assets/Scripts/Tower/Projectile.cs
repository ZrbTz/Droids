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
        Vector3 direction = target.body.position - transform.position; direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        Transform root = other.gameObject.transform.root;
        if (root.TryGetComponent(out NewInvoker newInvoker)) {
                newInvoker.health -= damage*0.1f;
                Destroy(gameObject);
            return;
        }

        if (root.TryGetComponent(out NewBomber newbomber)) {
            newbomber.health -= damage * 0.5f;
            Destroy(gameObject);
            return;
        }

        if (other.transform.root.GetComponent<Unit>() == target) {
            target.health -= damage;
            Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision) {
        if (collision.collider.transform.root.GetComponent<Unit>() == target) {
            target.health -= damage;
            Destroy(gameObject);
        }
    }*/
}
