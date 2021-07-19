using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile_Inseguimento : MonoBehaviour
{
    public float velocitaProiettile = 10.0f;
    public float rotazioneProrietile = 1000.0f;
    public Transform target;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direzione = target.position - transform.position;
        direzione.Normalize();

        Vector3 rotazione = Vector3.Cross(transform.forward, direzione);
        rb.angularVelocity = rotazione * rotazioneProrietile;

        rb.velocity = transform.forward * velocitaProiettile;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(this);
    }
}
