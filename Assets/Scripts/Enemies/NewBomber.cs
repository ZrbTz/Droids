using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NewBomber : Enemy {

    private GameObject player;
    [SerializeField] private GameObject bomb;
    public enum BomberState {
        Marching,
        Shooting
    }

    public BomberState state;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private float shootRange = 12.5f;
    [SerializeField] private float shootCooldown = 7.5f;
    [SerializeField] private float shootDelay = 5f;
    private float shootTime;
    private bool shoot;

    protected override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        nexus = GameManager.Instance.nexus;
        enemy = true;
        shoot = false;
        shootTime = Time.time;
        randomArea = Map.Instance.GetRandomArea();
        StartMarching();
        navMeshAgent.updateRotation = true;
    }

    protected override void Update() {
        base.Update();
        if (dead)
            return;
        switch (state) {
            case BomberState.Marching:
                animator.SetFloat("Speed", navMeshAgent.speed);
                if (CanShoot()) {
                    StopMarching();
                    StartShooting();
                }
                break;
            case BomberState.Shooting:
                break;
        }
    }

    private bool CanShoot() {
        int layerMask = ~LayerMask.GetMask("Player", "AreaEffect", "Projectile", "Item");
        Vector3 target = player.transform.position + new Vector3(0, 1.5f, 0);
        if (Vector3.Distance(transform.position, player.transform.position) >= shootRange 
            || Physics.Linecast(transform.position, target, layerMask, QueryTriggerInteraction.Ignore))
            return false;
        else if (!shoot)
            return Time.time - shootTime >= shootDelay;
        else
            return Time.time - shootTime >= shootCooldown;
    }

    public void StartMarching() {
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = destination.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = BomberState.Marching;
    }

    private void StopMarching() {
        navMeshAgent.isStopped = true;
    }

    override protected void addTarget(Obstacle bersaglio) { }

    override protected void removeTarget(Obstacle bersaglio) { }

    public void StartShooting() {
        state = BomberState.Shooting;
        animator.SetTrigger("Shoot");
    }

    public void Shoot() {
        //Instantiate(bomb, player.transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        int layerMask = ~LayerMask.GetMask("Player", "AreaEffect", "Projectile", "Item");
        Vector3 target = player.transform.position + new Vector3(0, 1.5f, 0);
        if (!Physics.Linecast(transform.position, target, layerMask, QueryTriggerInteraction.Ignore)) {
            RaycastHit hit;
            Physics.Raycast(player.transform.position + player.transform.up * 10 + bomb.transform.position, transform.TransformDirection(-1 * Vector3.up), out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore);
            Instantiate(bomb, hit.point, bomb.transform.rotation);
        }
        shootTime = Time.time;
        shoot = true;
        animator.ResetTrigger("Shoot");
    }
}
