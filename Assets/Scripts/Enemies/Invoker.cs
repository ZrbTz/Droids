using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*[RequireComponent(typeof(NavMeshAgent))]
public class Invoker : Enemy {
    private enum SoldierState {
        Marching,
        Attacking,
        Idle
    }

    private SoldierState state;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject soldierToSpawn;

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
                if (Distance(destination) <= attackRange) {
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
                if(Distance(destination) > attackRange) {
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
        switch (state) {
            case SoldierState.Marching:
                break;
            case SoldierState.Attacking:
                if (Time.time - attackTime >= 1 / attackSpeed) {
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
        navMeshAgent.destination = destination.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = SoldierState.Marching;
    }

    private void StopMarching() {
        navMeshAgent.isStopped = true;
    }

    override protected void addTarget(Obstacle bersaglio) {
        targets.Add(bersaglio);
        StartAttacking();
    }

    override protected void removeTarget(Obstacle bersaglio) {
        targets.Remove(bersaglio);
        if (targets.Count > 0) {
            StartAttacking();
        }
    }

    private void StartAttacking() {
        StopMarching();
        state = SoldierState.Attacking;
    }

    private void StopAttacking() {
        if (targets.Count > 0) {
            StartAttacking();
            return;
        }
        StartMarching();
    }

    private void Attack() {
        GameObject newEnemy = Instantiate(soldierToSpawn, transform.position + 2*transform.forward, transform.rotation);
        newEnemy.GetComponent<Enemy>().destination = this.destination;
    }

    protected override void Die() {
        switch (state) {
            case SoldierState.Attacking:
                StopAttacking();
                break;
            case SoldierState.Marching:
                StopMarching();
                break;
        }
        state = SoldierState.Idle;
        base.Die();
    }
}*/
