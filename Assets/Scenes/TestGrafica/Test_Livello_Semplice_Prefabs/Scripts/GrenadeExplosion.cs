using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public bool puoEsplodere = false;
    public int danno = 3;
    public float raggio = 3.0f;
    public GameObject esplosione;
    public float delayEsplosione = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (puoEsplodere && other.GetComponent<Enemy>()!=null)
        {
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(delayEsplosione);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, raggio);
        GameObject newPlaced = Instantiate(esplosione, GetComponent<Transform>().position, Quaternion.identity);
        Destroy(newPlaced, 0.2f);
        foreach (Collider c in hitColliders)
        {
            if (c.GetComponent<Enemy>() != null)
            {
                c.GetComponent<Unit>().health -= danno;
            }
        }
        Destroy(this.transform.parent.gameObject);
        Destroy(this.gameObject);

        yield break;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(puoEsplodere)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, raggio);
            GameObject newPlaced = Instantiate(esplosione);
            Destroy(newPlaced, 0.2f);
            foreach (Collider c in hitColliders)
            {
                if(c.GetComponent<Enemy>() != null)
                {
                    c.GetComponent<Unit>().health -= danno;
                }
            }
        }
    }*/

}
