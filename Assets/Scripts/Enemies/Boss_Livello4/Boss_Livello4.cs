using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Livello4 : Enemy
{
    public float intervalloProiettili = 4.0f;
    public Proiettile_Inseguimento missile;
    public Transform spawnProiettili;
    public Transform player;
    public GameObject areaRallentante;


    private int vita = 3;
    private IEnumerator sparaProiettili;
    private IEnumerator ondataFinale;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        navMeshAgent.destination = path[0].transform.position;
        sparaProiettili = coroutineSparaMissile(intervalloProiettili, -1);
        ondataFinale = coroutineSparaMissile(intervalloProiettili/10, 10);
    }

    IEnumerator coroutineSparaMissile(float intervalloProiettili, int numProiettili)
    {
        int numSparati = 0;
        while (numProiettili < 0 || numProiettili > numSparati)
        {
            sparaMissile();
            numSparati++;
            yield return new WaitForSeconds(intervalloProiettili);
        }
    }

    void sparaMissile()
    {
        Proiettile_Inseguimento p = Instantiate(missile, spawnProiettili);
        p.target = player;
    }

    public override float updatePath(GameObject currentlyColliding)
    {
        float durata_sosta = base.updatePath(currentlyColliding);
        if(durata_sosta > 0.1f)
        {
            Debug.Log("sosta");
            StartCoroutine(arrestaMovimento(durata_sosta));
        }
        return 0.0f;
    }

    IEnumerator arrestaMovimento (float durata_sosta)
    {
        navMeshAgent.isStopped = true;
        //arresta animazione  o loop idle animation
        yield return new WaitForSeconds(durata_sosta);
        //riprendi animazione
        navMeshAgent.isStopped = false;
    }

    public void danneggia ()
    {
        vita--;
        switch(vita)
        {
            case 2:
                areaRallentante.SetActive(true);
                break;
            case 1:
                StartCoroutine(sparaProiettili);
                break;
            case 0:
                StopCoroutine(sparaProiettili);
                StartCoroutine(ondataFinale);
                Die( intervalloProiettili );
                break;
        }

    }
}