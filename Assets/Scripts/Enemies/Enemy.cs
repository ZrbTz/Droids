using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {
    public bool hitNexus = false;
    public float nexusDamage = 1f;
    public GameObject destination;
    protected int randomArea;

    public float fadeSpeed = 1.0f;
    /*
    private Renderer[] rs;
    private List<Color> cs = new List<Color>();
    */
    public void FadeAndDisappear()
    {
        //in unity la trasparenza ha dei problemi con lo Z-buffer, il che produce artefatti significativi
        //StartCoroutine(LerpAlfa(fadeSpeed));
        this.Die(fadeSpeed);
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
        randomArea = Map.Instance.GetRandomArea();
        speedTemp = 0f;
        enemy = true;
    }

    private float speedTemp;
    [SerializeField] protected Animator animator;
    protected NavMeshAgent navMeshAgent;

    protected void UpdateAnimatorWalkSpeed() {
        if(navMeshAgent.speed != speedTemp) {
            animator.SetFloat("Speed", navMeshAgent.speed);
            speedTemp = navMeshAgent.speed;
        }
    }
}
