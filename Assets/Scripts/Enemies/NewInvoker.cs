using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
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
    private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject soldierToSpawn;

    private Animator animator;

    protected override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        nexus = GameManager.Instance.nexus;
        enemy = true;
        spawn = false;
        spawnTime = Time.time;
        randomArea = Map.Instance.GetRandomArea();
        StartMarching();
    }

    protected override void Update() {
        base.Update();
        if (dead)
            return;
        switch (state) {
            case InvokerState.Marching:
                animator.SetFloat("Speed", navMeshAgent.speed);
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
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = destination.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = InvokerState.Marching;
    }

    private void StopMarching()
    {
        this.GetComponent<EnemyAnimationSounds>().StopEngine();
        navMeshAgent.isStopped = true;
        state = InvokerState.Idle;
        animator.SetBool("Spawning", true);
    }

    private void StartAttacking() { }

    private void StopAttacking() { }

    private void SpawnSoldier() {
        GameObject newEnemy = Instantiate(soldierToSpawn, spawnTransform.position, transform.rotation);
        newEnemy.GetComponent<Enemy>().destination = this.destination;
    }
}
