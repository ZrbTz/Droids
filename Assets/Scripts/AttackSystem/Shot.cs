using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float damage = 30.0f;
    public float timeToLive = 5f;
    public float shotgunTimeToLive = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Invoker isInvoker(GameObject g) {
        Invoker invoker = g.transform.root.GetComponent<Invoker>();
        if (invoker != null) return invoker;

        invoker = g.transform.root.GetComponent<Invoker>();
        return invoker;
    }

    private void OnCollisionEnter(Collision collision) {
        Transform root = collision.transform.root;
        if (root.TryGetComponent(out Invoker invoker)) {
            if (collision.collider.gameObject.name == "Core") {
                invoker.health -= damage;
                Destroy(gameObject);
            }
            return;
        }

        if(root.TryGetComponent(out NewInvoker newInvoker)) {
            if (collision.collider.gameObject.name == "Core") {
                newInvoker.health -= damage;
                Destroy(gameObject);
            }
            return;
        }

        Unit enemy = collision.collider.transform.root.GetComponent<Unit>();
        if (enemy && enemy.enemy)
        {
            enemy.health -= damage;
            Destroy(gameObject);
            //Debug.Log("BECCATO");
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log(this.transform.position.ToString());
        }

    }
}
