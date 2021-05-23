using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bomber : Enemy {

    private GameObject player;
    [SerializeField] private GameObject bomb;
    private enum SoldierState {
        Marching,
        Attacking,
        Idle
    }

    private SoldierState state;
    private NavMeshAgent navMeshAgent;

    protected override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        nexus = GameManager.Instance.nexus;
        enemy = true;
        randomArea = Map.Instance.GetRandomArea();
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        if (dead)
            return;
        /*switch (state) {
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
        }*/
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

        if (Time.time - attackTime >= 1 / attackSpeed) {
            Attack();
            attackTime = Time.time;
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
        target.Add(bersaglio);
        StartAttacking();
    }

    override protected void removeTarget(Obstacle bersaglio) {
        target.Remove(bersaglio);
        if (target.Count > 0) {
            StartAttacking();
        }
    }

    private void StartAttacking() {
        StopMarching();

        //currentTarget = target[0];
        //Vector3 direction = currentTarget.transform.position - transform.position; direction.y = 0; direction.Normalize();
        //transform.rotation = Quaternion.LookRotation(direction);
        state = SoldierState.Attacking;
    }

    private void StopAttacking() {
        if (target.Count > 0) {
            StartAttacking();
            return;
        }
        StartMarching();
    }

    private void Attack() {
        //while (currentTarget == null) {
        //    target.Remove(target[0]);
        //    if (target.Count == 0) {
        //        StartMarching();
        //        return;
        //    }
        //    currentTarget = target[0];
        //}

        Instantiate(bomb, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        Debug.Log(player.GetComponent<Invector.vCharacterController.vThirdPersonController>().moveSpeed);
        //if (currentTarget.health <= 0) {
        //    target.Remove(currentTarget);
        //    StopAttacking();
        //}
    }

    //protected override void Die() {
    //    switch (state) {
    //        case SoldierState.Attacking:
    //            StopAttacking();
    //            break;
    //        case SoldierState.Marching:
    //            StopMarching();
    //            break;
    //    }
    //    state = SoldierState.Idle;
    //    base.Die();
    //}
}