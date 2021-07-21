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

    private Tower targetTower;
    private spiderState currentState;


    protected override void Start() {
        base.Start();
        currentState = spiderState.Hiding;
        targetTower = null;
        dropItem = this.gameObject.GetComponent<DropItem>();
        enemy = true;
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

    protected override void Update() {
        switch (currentState) {
            case spiderState.Hiding:
                GameObject target = getRandomTower();
                if (target != null) {
                    this.gameObject.transform.position = target.transform.position + new Vector3(0, 10, 0);
                    currentState = spiderState.Catching;
                }
                break;
            case spiderState.Escaping:
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
        }
    }
}
