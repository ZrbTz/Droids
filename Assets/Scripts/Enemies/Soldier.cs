﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*[RequireComponent(typeof(NavMeshAgent))]
public class Soldier : Enemy {
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
        nexus = GameManager.Instance.nexus;
        enemy = true;
        randomArea = Map.Instance.GetRandomArea();
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        if (dead)
            return;
        switch (state)
        {
            case SoldierState.Marching:
                break;
            case SoldierState.Attacking:
                if (Time.time - attackTime >= 1 / attackSpeed)
                {
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

    override protected void addTarget (Obstacle bersaglio)
    {
        target.Add(bersaglio);
        StartAttacking();
    }

    override protected void removeTarget(Obstacle bersaglio)
    {
        target.Remove(bersaglio);
        if(target.Count > 0)
        {
            StartAttacking();
        }
    }

    private void StartAttacking()
    {
        StopMarching();
        currentTarget = target[0];
        Vector3 direction = currentTarget.transform.position - transform.position; direction.y = 0; direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
        state = SoldierState.Attacking;
    }

    private void StopAttacking() {
        if(target.Count > 0)
        {
            StartAttacking();
            return;
        }
        StartMarching();
    }

    private void Attack() {
        while (currentTarget == null)
        {
            target.Remove(target[0]);
            if (target.Count == 0)
            {
                StartMarching();
                return;
            }
            currentTarget = target[0];
        }
        currentTarget.health -= damage;
        if (currentTarget.health <= 0)
        {
            target.Remove(currentTarget);
            StopAttacking();
        }
    }
}*/
