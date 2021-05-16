using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit {
    public enum TowerState {
        Idle,
        Rotating,
        Attacking
    }

    public float range = 7.5f;
    public float damage = 50f;
    public float fireRate = 1f;
    public float cannonAngularSpeed = 360f;
    public Transform cannonSphere;
    public Transform cannonMouth;
    public Projectile projectilePrefab;

    private Unit target = null;
    public TowerState state;
    private int buildingLayerMask;
    private float attackTime = Mathf.NegativeInfinity;
    private Transform nexus;
    private float cannonLength;

    protected override void Start() {
        base.Start();
        buildingLayerMask = 1 << LayerMask.NameToLayer("Building");
        nexus = GameManager.Instance.nexus.transform;
        state = TowerState.Idle;
        cannonLength = Vector3.Distance(cannonMouth.position, cannonSphere.position);
    }

    protected override void Update() {
        base.Update();
        if (dead)
            return;
        switch (state) {
            case TowerState.Idle:
                if (GetTarget()) 
                    state = TowerState.Rotating;
                break;
            case TowerState.Rotating:
                if (!CheckTarget() && !GetTarget()) {
                    state = TowerState.Idle;
                    break;
                }
                RotateWeapon();
                if (CannonTargetAngle() <= 5f)
                    state = TowerState.Attacking;
                
                break;
            case TowerState.Attacking:
                if (!CheckTarget()) {
                    if (GetTarget())
                        state = TowerState.Rotating;
                    else
                        state = TowerState.Idle;
                    break;
                }
                RotateWeapon();
                if (CannonTargetAngle() > 10f)
                    state = TowerState.Rotating;
                if (Time.time - attackTime >= 1 / fireRate) {
                    FireCannon();
                    attackTime = Time.time;
                }
                break;
        }
    }

    private bool GetTarget() {
        bool found = false;
        float minDistance = Mathf.Infinity;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hitCollider in hitColliders) {
            if (!(hitCollider.TryGetComponent<Unit>(out Unit unit) && !unit.dead && unit.enemy))
                continue;
            if (!LineOfFire(unit))
                continue;
            float distance = Vector3.Distance(unit.transform.position, nexus.position);
            if (distance <= minDistance) {
                target = unit;
                found = true;
                minDistance = distance;
            }
        }
        return found;
    }

    private void RotateWeapon() {
        float delta = cannonAngularSpeed * Time.deltaTime;
        Vector3 lookRotation = Quaternion.LookRotation(target.body.position - cannonSphere.position).eulerAngles;
        cannonSphere.rotation = Quaternion.RotateTowards(cannonSphere.rotation, Quaternion.Euler(lookRotation), delta);
    }

    private float CannonTargetAngle() {
        return Quaternion.Angle(cannonSphere.rotation, Quaternion.LookRotation(target.body.position - cannonSphere.position));
    }

    private bool LineOfFire(Unit unit) {
        float distance = Vector3.Distance(cannonSphere.position, unit.body.position);
        Ray ray = new Ray(cannonSphere.position, unit.body.position - cannonSphere.position);
        return !Physics.Raycast(ray, out RaycastHit hitInfo, distance, buildingLayerMask);
    }

    private void FireCannon() {
        Projectile projectile = Instantiate(projectilePrefab, cannonMouth.position, cannonMouth.rotation);
        projectile.target = target;
        projectile.damage = damage;
    }

    private bool CheckTarget() {
        return !target.dead && Distance(target.gameObject) <= range && LineOfFire(target);
    }
}
