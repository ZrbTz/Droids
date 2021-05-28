using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTower : Unit {
    public enum TowerState {
        Idle,
        Rotating,
        Attacking
    }

    public float range = 10f;
    public float damage = 50f;
    public float fireRate = 1f;
    public float cannonAngularSpeed = 360f;
    public Transform cannonBase;
    public Transform cannonPivot;
    public Transform cannonMouth;
    public Projectile projectilePrefab;
    private Unit target = null;
    public TowerState state;
    public LayerMask lineOfFireLayerMask;
    private float attackTime = Mathf.NegativeInfinity;
    private Transform nexus;

    protected override void Start() {
        base.Start();
        //lineOfFireLayerMask = 1 << LayerMask.NameToLayer("Building");
        nexus = GameManager.Instance.nexus.transform;
        state = TowerState.Idle;
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
            if (!(hitCollider.transform.root.TryGetComponent(out Unit unit) && !unit.dead && unit.enemy))
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
        float maxDelta = cannonAngularSpeed * Time.deltaTime;
        Vector3 lookRotation = Quaternion.LookRotation(target.body.position - cannonPivot.position).eulerAngles;
        lookRotation = WorldRotationToLocal(lookRotation);
        Vector3 baseTargetRotation = new Vector3(0f, lookRotation.y, 0f);
        Vector3 pivotTargetRotation = new Vector3(lookRotation.x, 0f, lookRotation.z);
        cannonBase.localRotation = Quaternion.RotateTowards(cannonBase.localRotation, Quaternion.Euler(baseTargetRotation), maxDelta);
        cannonPivot.localRotation = Quaternion.RotateTowards(cannonPivot.localRotation, Quaternion.Euler(pivotTargetRotation), maxDelta);
    }

    private Vector3 WorldRotationToLocal(Vector3 angles) {
        Transform child = transform.GetChild(0);
        Quaternion temp = child.rotation;
        child.eulerAngles = angles;
        angles = child.localEulerAngles;
        child.rotation = temp;
        return angles;
    }

    private float CannonTargetAngle() {
        return Quaternion.Angle(cannonPivot.rotation, Quaternion.LookRotation(target.body.position - cannonPivot.position));
    }

    private bool LineOfFire(Unit unit) {
        float distance = Vector3.Distance(cannonPivot.position, unit.body.position);
        Ray ray = new Ray(cannonPivot.position, unit.body.position - cannonPivot.position);
        return !Physics.Raycast(ray, out RaycastHit hitInfo, distance, lineOfFireLayerMask);
    }

    private void FireCannon() {
        Projectile projectile = Instantiate(projectilePrefab, cannonMouth.position, cannonMouth.rotation);
        projectile.target = target;
        projectile.damage = damage;
    }

    private bool CheckTarget() {
        return !target.dead && Vector3.Distance(transform.position, target.transform.position) <= range && LineOfFire(target);
    }
}
