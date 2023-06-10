using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
    Enemy, Nexus, Player, None
}

public class Unit : MonoBehaviour{

    private float maxHealth;
    public float health = 100f;
    [HideInInspector] public UnitType type = UnitType.None;
    public bool dead = false;

    public Transform body;
    public ParticleSystem destructionParticle;

    private new Collider collider;

    protected virtual void Start()
    {
        collider = GetComponent<Collider>();
        if (collider == null)
        {

        }

        maxHealth = health;
    }

    protected virtual void Update()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            DropItem di = this.GetComponent<DropItem>();
            if (di != null)
            {
                di.Drop();
            }
            Die();
        }
    }

    public float Distance(GameObject other)
    {
        Vector3 p1 = collider.ClosestPoint(other.transform.position);
        Collider p = other.GetComponent<Collider>();
        if (p == null) return 1000f;
        Vector3 p2 = p.ClosestPoint(transform.position);
        return Vector3.Distance(p1, p2);
    }

    protected virtual void Die(){
        Destroy(gameObject);
    }

    protected virtual void Die(float timeToDie)
    {
        dead = true;
        Destroy(gameObject, timeToDie);
        //Instantiate(destructionParticle, body.position, body.rotation);
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void Damage(float damage) {
        health -= damage;
        OnDamage(damage);
    }

    public virtual void OnDamage(float damage) { }
}
