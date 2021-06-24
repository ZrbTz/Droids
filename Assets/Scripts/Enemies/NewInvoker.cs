using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewInvoker : Enemy {
    public enum InvokerState {
        Marching,
        Spawning,
        Idle
    }

    [SerializeField] private int spawnCount;
    [SerializeField] private int spawnInterval;
    [SerializeField] private int spawnCooldown;
    [SerializeField] private int spawnDelay;
    [SerializeField] private Transform spawnTransform;
    private bool spawn;
    private float spawnTime;
    private int spawnIndex;
    public InvokerState state;
    [SerializeField] private GameObject soldierToSpawn;


    protected override void Start() {
        base.Start();
        enemy = true;
        spawn = false;
        spawnTime = Time.time;
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        if (dead)
            return;
        switch (state) {
            case InvokerState.Marching:
                UpdateAnimatorWalkSpeed();
                if (CanSpawn())
                    StopMarching();
                break;
            case InvokerState.Spawning:
                if(Time.time - spawnTime >= spawnInterval) {
                    SpawnSoldier();
                    spawnTime = Time.time;
                    spawnIndex++;
                    if (spawnIndex >= spawnCount)
                        StopSpawning();
                }
                break;
            case InvokerState.Idle:
                break;
        }
    }

    private bool CanSpawn() {
        if (!spawn)
            return Time.time - spawnTime >= spawnDelay;
        else
            return Time.time - spawnTime >= spawnCooldown;
    }

    public void StartSpawning() {
        state = InvokerState.Spawning;
        spawn = true;
        spawnIndex = 1;
    }

    private void StopSpawning() {
        animator.SetBool("Spawning", false);
        state = InvokerState.Idle;
    }

    public void StartMarching() {
        marching = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = destination.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = InvokerState.Marching;
        UpdateAnimatorWalkSpeed();
    }

    private void StopMarching()
    {
        marching = false;
        this.GetComponent<EnemyAnimationSounds>().StopEngine();
        navMeshAgent.isStopped = true;
        state = InvokerState.Idle;
        animator.SetBool("Spawning", true);
    }

    private void StartAttacking() { }

    private void StopAttacking() { }

    private void SpawnSoldier() {
        GameObject newEnemy = Instantiate(soldierToSpawn, spawnTransform.position, transform.rotation);
        Enemy enemy = newEnemy.GetComponent<Enemy>();
        enemy.destination = this.destination;
        enemy.path = this.path;
        enemy.passedPath = this.passedPath;
    }
}
