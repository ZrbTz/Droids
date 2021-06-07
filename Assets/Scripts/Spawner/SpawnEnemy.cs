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
    public HordeDifficulty difficulty;
}

public enum HordeDifficulty {
    None,
    Easy,
    Normal,
    Hard
}

[System.Serializable]
public class BigHorde {
    public Horde[] hordes;
}

public class SpawnEnemy : MonoBehaviour
{
    private GameManager gm;
    public SO_BigHordes so_BigHordes_Easy;
    public SO_BigHordes so_BigHordes;
    public SO_BigHordes so_BigHordes_Hard;
    private BigHorde[] bigHordes;

    public GameObject destination;

    [SerializeField] float block_width = 5.0f;
    [SerializeField] float block_length = 5.0f;
    [SerializeField] int maxtimeSpawn = 2;
    List<GameObject> spawnFreeZones = new List<GameObject>();

    BoxCollider _spawnZoneCollider;
    MiniMapSpawner miniMapSpawner;
    [SerializeField] GameObject SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.ignoreDifficulty)
            this.bigHordes = so_BigHordes.bigHordes;
        else {
            switch (GameManager.Instance.difficulty) {
                case GameDifficulty.Easy:
                    bigHordes = so_BigHordes_Easy.bigHordes;
                    break;
                case GameDifficulty.Normal:
                    bigHordes = so_BigHordes.bigHordes;
                    break;
                case GameDifficulty.Hard:
                    bigHordes = so_BigHordes_Hard.bigHordes;
                    break;
            }
        }

        gm = GameManager.Instance;
        _spawnZoneCollider = this.gameObject.GetComponent<BoxCollider>();
        miniMapSpawner = GetComponent<MiniMapSpawner>();
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
        AddSpawnDataToMiniMap(bigHorde);
        for (int j = 0; j < bigHorde.hordes.Length; j++)
        {

            yield return new WaitForSeconds(bigHorde.hordes[j].delay);
            float elapsedTime = 0.0f;
            float totTime = bigHorde.hordes[j].tempoPerSpawnare;
            int counter = 0;
            while (elapsedTime < totTime)
            {
                //percentuale tempo passata > percentuale nemici spawnati
                if (elapsedTime / totTime > (float)counter / bigHorde.hordes[j].count)
                {
                    Vector3 spawnPosition = SpawnPoint.transform.position;
                    spawnPosition.x = spawnPosition.x + Random.Range(-3.0f, 3.0f);
                    spawnPosition.z = spawnPosition.z + Random.Range(-3.0f, 3.0f);
                    
                    GameObject newEnemy = Instantiate(bigHorde.hordes[j].enemy, spawnPosition, Quaternion.identity);
                    newEnemy.GetComponent<Enemy>().destination = destination;

                    counter++;
                }
                elapsedTime += Time.deltaTime;
                yield return new WaitForFixedUpdate();
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

    private void AddSpawnDataToMiniMap(BigHorde bigHorde) {
        float time = Time.time;
        foreach (Horde horde in bigHorde.hordes) {
            time += horde.delay;
            if (horde.difficulty != HordeDifficulty.None)
                miniMapSpawner.AddSpawnData(time, horde.difficulty);
            time += horde.tempoPerSpawnare;
        }
    }
}
