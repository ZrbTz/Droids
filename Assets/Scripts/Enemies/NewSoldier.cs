using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class NewSoldier : Enemy {
    private enum SoldierState {
        Marching,
        Attacking,
        Approaching
    }

    private SoldierState state;
    private Obstacle farTarget;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private float obstacleScanIntervalMin = 0.5f;
    [SerializeField] private float obstacleScanIntervalMax = 0.75f;
    [SerializeField] private const float obstacleScanRadius = 7.5f;
    [SerializeField] private const float obstacleScanAngle = 35f;
    private float obstacleScanTime;
    private float obstacleScanInterval;
    private List<Obstacle> targets = new List<Obstacle>();
    private Obstacle currentTarget;
    [SerializeField] private float attackDamage = 1f;

    protected override void Start() {
        base.Start();
        obstacleScanTime = -Mathf.Infinity;
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        switch (state) {
            case SoldierState.Approaching:
                UpdateAnimatorWalkSpeed();
                if (GetTarget()) {
                    StopMarching();
                    StartAttacking();
                } else if (farTarget == null || farTarget.health <= 0)
                    StartMarching();
                break;
            case SoldierState.Marching:
                UpdateAnimatorWalkSpeed();
                if (Time.time - obstacleScanTime >= obstacleScanInterval && GetFarTarget()) {
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
        UpdateAnimatorWalkSpeed();
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
        animator.gameObject.GetComponent<EnemyAnimationSounds>().StopEngine();
    }

    private void StartAttacking() {
        Vector3 direction = currentTarget.transform.position - transform.position; direction.y = 0; direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
        state = SoldierState.Attacking;
        animator.SetBool("Attacking", true);
    }

    private bool GetTarget() {
        if (targets.Count <= 0)
            return false;
        targets = targets.Where(obstacle => obstacle != null && obstacle.health > 0).ToList();
        if (targets.Count > 0) {
            currentTarget = targets[0];
            return true;
        } else
            return false;
    }

    private bool GetFarTarget() {
        var colliders = Physics.OverlapSphere(transform.position, obstacleScanRadius, obstacleLayerMask);
        float minDistance = Mathf.Infinity;
        farTarget = null;
        foreach(var collider in colliders) {
            Vector3 direction = collider.transform.position - transform.position; direction.y = 0;
            if (collider.TryGetComponent(out Obstacle obstacle) && obstacle.health > 0
            && Vector3.Dot(direction.normalized, transform.forward) > Mathf.Cos(obstacleScanAngle * 0.5f * Mathf.Deg2Rad)
            && Vector3.Distance(transform.position, obstacle.transform.position) < minDistance)
            farTarget = obstacle;
        }
        obstacleScanTime = Time.time;
        obstacleScanInterval = Random.Range(obstacleScanIntervalMin, obstacleScanIntervalMax);
        return (farTarget != null);
    }

    private void StopAttacking() {
        animator.SetBool("Attacking", false);
    }

    public void Attack() {
        currentTarget.health -= attackDamage;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Obstacle obstacle))
            targets.Add(obstacle);
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Obstacle obstacle))
            targets.Remove(obstacle);
    }
}
