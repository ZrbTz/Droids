using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float damage = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Unit enemy = collision.collider.GetComponent<Unit>();
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
