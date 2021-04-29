using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Soldier : Unit {
    private enum SoldierState {
        Marching,
        Attacking,
        Idle
    }

    public float attackSpeed = 1f;
    public float attackRange = .5f;
    public float damage = 10f;
    private Unit nexus;
    private NavMeshAgent navMeshAgent;
    private SoldierState state;
    private float attackTime;
    private int randomArea;

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
        if (dead)
            return;
        switch (state) {
            case SoldierState.Marching:
                if (nexus.dead) {
                    StopMarching();
                    state = SoldierState.Idle;
                    break;
                }
                if (Distance(nexus) <= attackRange) {
                    StopMarching();
                    StartAttacking();
                    break;
                }
                break;
            case SoldierState.Attacking:
                if(nexus.dead) {
                    StopAttacking();
                    state = SoldierState.Idle;
                    break;
                }
                if(Distance(nexus) > attackRange) {
                    StopAttacking();
                    StartMarching();
                    break;
                }
                if(Time.time - attackTime >= 1 / attackSpeed) {
                    Attack();
                    attackTime = Time.time;
                }
                break;
            case SoldierState.Idle:
                break;
        }
    }

    private void StartMarching() {
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = nexus.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = SoldierState.Marching;
    }

    private void StopMarching() {
        navMeshAgent.isStopped = true;
    }

    private void StartAttacking() {
        Vector3 direction = nexus.transform.position - transform.position; direction.y = 0; direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
        state = SoldierState.Attacking;
    }

    private void StopAttacking() {

    }

    private void Attack() {
        nexus.health -= damage;
    }

    protected override void Die() {
        base.Die();
        switch (state) {
            case SoldierState.Attacking:
                StopAttacking();
                break;
            case SoldierState.Marching:
                StopMarching();
                break;
        }
        state = SoldierState.Idle;
    }
}
