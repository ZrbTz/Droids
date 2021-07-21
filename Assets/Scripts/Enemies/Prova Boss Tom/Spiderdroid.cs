using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderdroid : Unit {
    private enum spiderState {
        Hiding, Catching, Escaping
    }

    public override float health {
        get => _health;
        set {
            _health = value;
            if (_health <= 0 && !dead) {
                dead = true;
                dropItem.Drop();
                Die();
            }
        }
    }

    //A list for the moment, could be better with a graph
    [SerializeField] private GameObject[] jumpingGraph;
    [SerializeField] private GameObject hidingSpot;
    private DropItem dropItem;

    //private Tower targetTower;
    private spiderState currentState;
    private Rigidbody rb;


    protected override void Start() {
        base.Start();
        currentState = spiderState.Hiding;
       // targetTower = null;
        dropItem = this.gameObject.GetComponent<DropItem>();
        enemy = true;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private float searchcountdown = 1f;
    GameObject getRandomTower() {
        searchcountdown -= Time.deltaTime;
        if (searchcountdown <= 0f) {
            searchcountdown = 5f;
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
            if (towers.Length == 0) return null;
            return towers[Random.Range(0, towers.Length)];
        }
        return null;
    }

    public float jumpTimer = 5f;
    private int jumpCounter = 0;
    private float speed = 50f;
    protected override void Update() {
        switch (currentState) {
            case spiderState.Hiding:
                GameObject target = getRandomTower();
                if (target != null) {
                    this.gameObject.transform.position = target.transform.position + new Vector3(0, 10, 0);
                    currentState = spiderState.Catching;
                }
                break;
            case spiderState.Catching:
                break;
            case spiderState.Escaping:
                jumpTimer -= Time.deltaTime;
                if(jumpTimer <= 0) {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, jumpingGraph[jumpCounter].transform.position, step);
                    if(Vector3.Distance(transform.position, jumpingGraph[jumpCounter].transform.position) < 0.001f) {
                        jumpTimer = 5f;
                        jumpCounter++;
                        if (jumpCounter == jumpingGraph.Length) jumpCounter = 0;
                    }
                }
                break;
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if(currentState == spiderState.Catching && collision.gameObject.CompareTag("Tower") && dropItem.toDrop == null) {
            Pickable tower = collision.gameObject.GetComponent<Pickable>();
            TowerItem t = (TowerItem)tower.getItemObject();
            dropItem.toDrop = t.GetPlaceableItemPrefab();
            Destroy(tower.gameObject);
            currentState = spiderState.Escaping;
            jumpTimer = 5f;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
