using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    private SpawnEnemy _spawnArea;
    private int enemyCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _spawnArea = this.transform.parent.gameObject.GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        enemyCounter++;
        _spawnArea.RemoveFromList(this.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        enemyCounter--;
        if (enemyCounter == 0)
        {
            _spawnArea.AddToList(this.gameObject);
        }
    }
}
