using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {
    static int priorityCount = 0;
    public bool hitNexus = false;
    public float nexusDamage = 1f;
    public float attackDamage = 1f;
    public GameObject destination;
    protected int randomArea;
    public bool marching = true;
    public ParticleSystem damageParticle;
    public ParticleSystem attackParticle;

    protected float attackRadius = 2f;
    private int layerMask;
    protected float attackTimeout = 2f;
    protected float attackTimer = 0f;
    protected float knockBackForce = 500f;
    protected float attackDelay = 1f;

    public string enemyName;
    public Sprite enemyIcon;

    public float fadeSpeed = 1.0f;

    public GameObject[] path;
    public int passedPath = 0;

    public int moneyValue = 0;
    /*
    private Renderer[] rs;
    private List<Color> cs = new List<Color>();
    */
    public void FadeAndDisappear(){
        //in unity la trasparenza ha dei problemi con lo Z-buffer, il che produce artefatti significativi
        //StartCoroutine(LerpAlfa(fadeSpeed));
        this.Die(fadeSpeed);
    }

    protected override void Die() {
        GameManager.Instance.coins += moneyValue;
        base.Die();
    }

    /*
    IEnumerator LerpAlfa(float lerpDuration)
    {

        float timeElapsed = 0;
        Color startingColor = this.GetComponent<Renderer>().material.color;

        rs = this.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rs.Length; i++)
        {
            cs.Add(rs[i].material.color);
        }

        while (timeElapsed < lerpDuration)
        {
            for (int i = 0; i < rs.Length; i++)
            {
                rs[i].material.color = Color.Lerp(cs[i], new Color(cs[i].r, cs[i].g, cs[i].b, 0), timeElapsed / lerpDuration);
            }
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
    }
    */

    protected override void Start() {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.avoidancePriority = priorityCount++;
        if (priorityCount == 99) priorityCount = 0;
        randomArea = Map.Instance.GetRandomArea();
        speedTemp = 0f;
        type = UnitType.Enemy;
        layerMask = LayerMask.GetMask("Player");
    }

    private float speedTemp;
    [SerializeField] protected Animator animator;
    public NavMeshAgent navMeshAgent;

    protected void UpdateAnimatorWalkSpeed() {
        if(navMeshAgent.speed != speedTemp) {
            animator.SetFloat("Speed", navMeshAgent.speed);
            speedTemp = navMeshAgent.speed;
        }
    }

    public void updatePath(GameObject currentlyColliding) {
        if (path[passedPath] == currentlyColliding) {
            passedPath++;
        }
    }

    public float GetPathRemainingDistance() {
        float distance = Vector3.Distance(this.transform.position, path[passedPath].transform.position);
        return distance;
    }

    public override void OnDamage(float damage) {
        if (damageParticle != null) {
            damageParticle.time = 0f;
            damageParticle.Play();
        }
    }

    protected void TryToAttackPlayer() {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackTimeout) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, layerMask);
            if (hitColliders.Length > 0) {
                StartCoroutine(AttackPlayer(attackDelay));
                var attackParticleInstance = Instantiate(attackParticle);
                attackParticleInstance.transform.SetParent(body);
                attackParticleInstance.transform.localPosition = Vector3.zero;
                attackTimer = 0;
            }
        }
    }

    IEnumerator AttackPlayer(float attackDelay) {
        yield return new WaitForSeconds(attackDelay);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, layerMask);
        if (hitColliders.Length > 0) {
            PlayerUnit playerUnit = hitColliders[0].GetComponent<PlayerUnit>();
            playerUnit.Damage(attackDamage);
        }
    }

    protected override void Update() {
        base.Update();
        TryToAttackPlayer();
    }

    protected override void Die(float timeToDie) {
        base.Die(timeToDie);
        Instantiate(destructionParticle, body.position, body.rotation);
    }
}
