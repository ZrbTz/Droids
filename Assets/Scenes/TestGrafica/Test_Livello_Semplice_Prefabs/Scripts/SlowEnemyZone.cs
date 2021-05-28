using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowEnemyZone : MonoBehaviour
{
    public float slowPercentuale = 0.5f;
    List<Collider> affected = new List<Collider>();


    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.GetComponent<Enemy>() != null)
        {
            var navMeshAgent = other.transform.root.GetComponent<NavMeshAgent>();
            navMeshAgent.speed *= (1 - slowPercentuale);
            affected.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.GetComponent<Enemy>() != null)
        {
            var navMeshAgent = other.transform.root.GetComponent<NavMeshAgent>();
            navMeshAgent.speed /= (1 - slowPercentuale);
            affected.Remove(other);
        }
    }

    private void OnDestroy() {
        foreach(Collider c in affected)
            if(c != null) c.transform.root.GetComponent<NavMeshAgent>().speed = c.transform.root.GetComponent<NavMeshAgent>().speed / (1 - slowPercentuale);
    }
}
