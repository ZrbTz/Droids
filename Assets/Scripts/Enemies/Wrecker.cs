using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Wrecker : Enemy {

    private enum SoldierState {
        Marching,
        Attacking,
        Idle
    }

    private SoldierState state;
    private DropItem dropItem;

    protected override void Start() {
        if (true || state == SoldierState.Marching) {
            base.Start();
            dropItem = this.gameObject.GetComponent<DropItem>();
            randomArea = Map.Instance.GetRandomArea();
            StartMarching();
        }
    }

    private void StartMarching() {
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = destination.transform.position;
        navMeshAgent.SetAreaCost(randomArea, 1f);
        state = SoldierState.Marching;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Tower") && dropItem.toDrop == null) {
                Pickable tower = other.gameObject.GetComponent<Pickable>();
                //DropItem dropItem = this.gameObject.GetComponent<DropItem>();
                TowerItem t = (TowerItem)tower.getItemObject();
                dropItem.toDrop = t.GetPlaceableItemPrefab();
                //Inventory inventory = this.gameObject.GetComponent<Inventory>();
                tower.Interact(this.gameObject);
                this.gameObject.GetComponent<SphereCollider>().enabled = false;
            }
        }
}
