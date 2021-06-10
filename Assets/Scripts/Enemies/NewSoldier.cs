using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NewSoldier : Enemy {
    private enum SoldierState {
        Marching,
        Attacking,
        Approaching
    }

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    private SoldierState state;
    private Obstacle farTarget;

    protected override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        nexus = GameManager.Instance.nexus;
        enemy = true;
        randomArea = Map.Instance.GetRandomArea();
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        switch (state) {
            case SoldierState.Approaching:
                animator.SetFloat("Speed", navMeshAgent.speed);
                if (GetTarget()) {
                    StopMarching();
                    StartAttacking();
                } else if (farTarget == null || farTarget.health <= 0)
                    StartMarching();
                break;
            case SoldierState.Marching:
                animator.SetFloat("Speed", navMeshAgent.speed);
                if (GetFarTarget()) {
                    StopMarching();
                    StartApproaching();
                }
                break;
            case SoldierState.Attacking:
                if (currentTarget == null || currentTarget.health <= 0) {
                    StopAttacking();
                    StartMarching();
                }
                break;
        }
    }

    private void StartMarching() {
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = destination.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = SoldierState.Marching;
    }

    private void StartApproaching() {
        navMeshAgent.isStopped = false;
        //navMeshAgent.destination = farTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
        navMeshAgent.destination = farTarget.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = SoldierState.Approaching;
    }

    private void StopMarching() {
        navMeshAgent.isStopped = true;
    }

    private void StartAttacking() {
        Vector3 direction = currentTarget.transform.position - transform.position; direction.y = 0; direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
        state = SoldierState.Attacking;
        animator.SetBool("Attacking", true);
    }

    private bool GetTarget() {
        if (target.Count <= 0)
            return false;
        target = target.Where(obstacle => obstacle != null && obstacle.health > 0).ToList();
        if (target.Count > 0) {
            currentTarget = target[0];
            return true;
        } else
            return false;
    }

    private bool GetFarTarget() {
        var colliders = Physics.OverlapSphere(transform.position, 12.5f);
        float minDistance = Mathf.Infinity;
        farTarget = null;
        foreach(var collider in colliders) {
            Vector3 direction = collider.transform.position - transform.position; direction.y = 0;
            if (collider.TryGetComponent(out Obstacle obstacle) && obstacle.health > 0
            && Vector3.Dot(direction.normalized, transform.forward) > Mathf.Cos(45f * 0.5f * Mathf.Deg2Rad)
            && Vector3.Distance(transform.position, obstacle.transform.position) < minDistance)
            farTarget = obstacle;
        }
        return (farTarget != null);
    }

    private void StopAttacking() {
        animator.SetBool("Attacking", false);
    }

    public void Attack() {
        currentTarget.health -= damage;
    }
}
