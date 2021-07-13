using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tower : Unit {
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
    public AudioClip shotSound;
    public AudioSource audioSource;
    private Unit target = null;
    public TowerState state;
    public LayerMask lineOfFireLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    private float attackTime = Mathf.NegativeInfinity;

    protected override void Start() {
        base.Start();
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
        int maxPassedPath = -100000;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
        foreach (Collider hitCollider in hitColliders) {
            if (!(hitCollider.transform.root.TryGetComponent(out Enemy enemy) && !enemy.dead && enemy.enemy))
                continue;
            if (!LineOfFire(enemy))
                continue;
            float distance = enemy.GetPathRemainingDistance();
            //Debug.Log(distance);
            //if (!enemy.marching) { distance = range; }
            if (enemy.passedPath > maxPassedPath || (enemy.passedPath == maxPassedPath && distance <= minDistance)) {
                target = enemy;
                found = true;
                minDistance = distance;
                maxPassedPath = enemy.passedPath;
            }
        }
        /*if (found)
        {
            Debug.Log(maxPassedPath);
            Debug.Log(minDistance);
        }*/
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
        audioSource.PlayOneShot(shotSound);
    }

    private bool CheckTarget() {
        if (target == null) return false;
        return !target.dead && Vector3.Distance(transform.position, target.transform.position) <= range && LineOfFire(target);
    }
}

//public static class ExtensionMethods {
//    public static float GetPathRemainingDistance(this NavMeshAgent navMeshAgent) {
//        if (navMeshAgent.pathPending || navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
//                navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
//                navMeshAgent.path.corners.Length == 0)
//            return 10000f;

//        float distance = 0.0f;
//        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i) {
//            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
//        }

//        return distance;
//    }
//}