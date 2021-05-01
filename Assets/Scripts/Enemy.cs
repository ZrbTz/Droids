using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{

    public float attackSpeed = 1f;
    public float attackRange = .5f;
    public float damage = 10f;
    protected Unit nexus;
    public GameObject destination;
    public List<Obstacle> target;
    public Obstacle currentTarget;
    protected float attackTime;
    protected int randomArea;

    public float fadeSpeed = 1.0f;
    public Renderer[] rs;
    public List<Color> cs = new List<Color>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>() != null)
        {
            
        }
    }

    protected virtual void addTarget(Obstacle o)
    {

    }

}
