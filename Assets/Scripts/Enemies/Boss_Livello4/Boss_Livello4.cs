using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Livello4 : MonoBehaviour
{
    public float intervalloProiettili = 4.0f;
    public Proiettile_Inseguimento missile;
    public Transform spawnProiettili;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sparaMissile());
    }

    IEnumerator sparaMissile()
    {
        while (true /*da sostiuire?*/) {
            Proiettile_Inseguimento p = Instantiate(missile, spawnProiettili);
            p.target = player;
            yield return new WaitForSeconds(intervalloProiettili);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
