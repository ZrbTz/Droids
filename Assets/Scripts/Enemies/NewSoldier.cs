using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NewSoldier : Enemy {
    private enum SoldierState {
        Marching,
        Attacking
    }

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private SoldierState state;

    protected override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nexus = GameManager.Instance.nexus;
        enemy = true;
        randomArea = Map.Instance.GetRandomArea();
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        switch (state) {
            case SoldierState.Marching:
                animator.SetFloat("Speed", navMeshAgent.speed);
                if (GetTarget()) {
                    StopMarching();
                    StartAttacking();
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

    private void StopAttacking() {
        animator.SetBool("Attacking", false);
    }

    private void Attack() {
        currentTarget.health -= damage;
    }
}
