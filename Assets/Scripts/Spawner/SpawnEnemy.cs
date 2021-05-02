using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Horde {
    public GameObject enemy;
    public int count;
    public float delay;
    public float tempoPerSpawnare = 5.0f;
}

[System.Serializable]
public class BigHorde {
    public Horde[] hordes;
}

public class SpawnEnemy : MonoBehaviour
{
    private GameManager gm;
    public BigHorde[] bigHordes;
    public GameObject destination;

    [SerializeField] float block_width = 5.0f;
    [SerializeField] float block_length = 5.0f;
    [SerializeField] int maxtimeSpawn = 2;
    List<GameObject> spawnFreeZones = new List<GameObject>();

    BoxCollider _spawnZoneCollider;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        _spawnZoneCollider = this.gameObject.GetComponent<BoxCollider>();
        Vector2 zoneNumber = new Vector2(Mathf.Floor(_spawnZoneCollider.size.x / block_width), Mathf.Floor(_spawnZoneCollider.size.z / block_length));
        for (int i = 0; i < zoneNumber[1]; i++)
        {
            for (int j = 0; j < zoneNumber[0]; j++)
            {
                GameObject zone = new GameObject();
                zone.name = "Zone" + i + "_" + j;
                zone.transform.parent = this.transform;
                zone.transform.localPosition = new Vector3(((1 - zoneNumber[0]) / 2 + j) * block_width, 0, ((1 - zoneNumber[1]) / 2 + i) * block_length);
                BoxCollider _zoneCollider = zone.AddComponent<BoxCollider>();
                _zoneCollider.size = new Vector3(block_width, _spawnZoneCollider.size.y, block_length);
                _zoneCollider.center = Vector3.up * _spawnZoneCollider.size.y / 2;
                _zoneCollider.isTrigger = true;
                zone.AddComponent<SpawnZone>();
                spawnFreeZones.Add(zone);
            }
        }
    }

    public void spawnHorde(int nextHorde) {
        //StartCoroutine(RandomicSpawn(bigHordes[nextHorde]));
        StartCoroutine(TimedSpawn(bigHordes[nextHorde]));
    }

    IEnumerator RandomicSpawn(BigHorde bigHorde)
    {
        Debug.Log("Start Spawning");
        for (int j = 0; j < bigHorde.hordes.Length; j++)
        {
            yield return new WaitForSeconds(bigHorde.hordes[j].delay);
            for (int i = 0; i < bigHorde.hordes[j].count; i++)
            {
                float timeToWait = Random.Range(0.0f, maxtimeSpawn);
                yield return new WaitForSeconds(timeToWait);
                while (spawnFreeZones.Count == 0)
                {
                    yield return new WaitForSeconds(1);
                }
                int indexZone = Random.Range(0, spawnFreeZones.Count - 1);
                GameObject selectedZone = spawnFreeZones[indexZone];
                spawnFreeZones.RemoveAt(indexZone);
                GameObject newEnemy = Instantiate(bigHorde.hordes[j].enemy);
                newEnemy.GetComponent<Soldier>().destination = destination;
                newEnemy.GetComponent<NavMeshAgent>().Warp(selectedZone.transform.TransformPoint(Vector3.zero));
                //newEnemy.transform.position = selectedZone.transform.TransformPoint(Vector3.zero);
                newEnemy.transform.rotation = this.transform.rotation;
            }
        }
        Debug.Log("Fine spawn");
        gm.signalSpawnEnd();
        yield break;
    }

    IEnumerator TimedSpawn(BigHorde bigHorde)
    {
        Debug.Log("Start Spawning");
        for (int j = 0; j < bigHorde.hordes.Length; j++)
        {
            yield return new WaitForSeconds(bigHorde.hordes[j].delay);
            float elapsedTime = 0.0f;
            float totTime = bigHorde.hordes[j].tempoPerSpawnare;
            int counter = 0;
            while (elapsedTime < totTime)
            {
                if (elapsedTime / totTime > counter / bigHorde.hordes[j].count)
                {
                    int indexZone = Random.Range(0, spawnFreeZones.Count - 1);
                    GameObject selectedZone = spawnFreeZones[indexZone];
                    GameObject newEnemy = Instantiate(bigHorde.hordes[j].enemy);
                    newEnemy.GetComponent<Enemy>().destination = destination;
                    newEnemy.GetComponent<NavMeshAgent>().Warp(selectedZone.transform.TransformPoint(Vector3.zero));
                    newEnemy.transform.rotation = this.transform.rotation;
                }
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(0);
            }
        }
        Debug.Log("Fine spawn");
        gm.signalSpawnEnd();
        yield break;
    }

    public void AddToList(GameObject zone)
    {
        if (spawnFreeZones.Contains(zone)) return;
        spawnFreeZones.Add(zone);
    }

    public void RemoveFromList(GameObject zone)
    {
        if (!spawnFreeZones.Contains(zone)) return;
        spawnFreeZones.Remove(zone);
    }
}
