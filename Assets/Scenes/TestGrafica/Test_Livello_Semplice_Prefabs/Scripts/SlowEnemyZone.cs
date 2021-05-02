using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowEnemyZone : MonoBehaviour
{
    public float slowPercentuale = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed * (1 - slowPercentuale);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<NavMeshAgent>().speed = other.GetComponent<NavMeshAgent>().speed / (1 - slowPercentuale);
        }
    }
}
