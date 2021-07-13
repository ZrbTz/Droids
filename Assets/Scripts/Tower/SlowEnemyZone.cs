using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowEnemyZone : MonoBehaviour {
    public float slowPercentuale = 0.5f;
    List<NavMeshAgent> affected = new List<NavMeshAgent>();


    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.GetComponent<Enemy>() != null) {
            var navMeshAgent = other.transform.root.GetComponent<NavMeshAgent>();
            if (affected.Contains(navMeshAgent))
                return;
            navMeshAgent.speed *= (1 - slowPercentuale);
            affected.Add(navMeshAgent);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.root.GetComponent<Enemy>() != null) {
            var navMeshAgent = other.transform.root.GetComponent<NavMeshAgent>();
            if (!affected.Contains(navMeshAgent))
                return;
            navMeshAgent.speed /= (1 - slowPercentuale);
            affected.Remove(navMeshAgent);
        }
    }

    private void OnDestroy() {
        foreach (NavMeshAgent a in affected)
            if (a != null) a.speed = a.speed / (1 - slowPercentuale);
    }
}
