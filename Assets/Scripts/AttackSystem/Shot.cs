using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float damage = 30.0f;
    public float timeToLive = 5f;
    public float shotgunTimeToLive = 0.2f;
    public ParticleSystem hitParticle;

    private bool hit;


    // Start is called before the first frame update
    void Start()
    {
        hit = false;
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
    }

    //private Invoker isInvoker(GameObject g) {
    //    Invoker invoker = g.transform.root.GetComponent<Invoker>();
    //    if (invoker != null) return invoker;

    //    invoker = g.transform.root.GetComponent<Invoker>();
    //    return invoker;
    //}

    private void OnCollisionEnter(Collision collision) {
        Transform root = collision.transform.root;
        //if (root.TryGetComponent(out Invoker invoker)) {
        //    if (collision.collider.gameObject.name == "Core") {
        //        invoker.health -= damage;
        //        Destroy(gameObject);
        //    }
        //    return;
        //}

        if (root.TryGetComponent(out Summoner newInvoker)) {
            if (collision.collider.gameObject.name == "Core")
                Hit(newInvoker, collision.contacts[0].point);
            return;
        }

        Unit enemy = collision.collider.transform.root.GetComponent<Unit>();
        if (enemy && enemy.enemy)
            Hit(enemy, collision.contacts[0].point);
        else
            Hit(collision.contacts[0].point);
    }

    private void Hit(Unit unit, Vector3 point) {
        if (!hit) { 
            unit.health -= damage;
            Hit(point);
            hit = true;
        }
    }

    private void Hit(Vector3 point) {
        Instantiate(hitParticle, point, transform.rotation);
        Destroy(gameObject);
    }
}
