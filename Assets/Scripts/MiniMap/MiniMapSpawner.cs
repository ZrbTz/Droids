using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapSpawner : MiniMapEntity {
    private class MapSpawnData {
        public float time;
        public Color color;
        public Image icon;

        public MapSpawnData(float time, Color color) {
            this.time = time;
            this.color = color;
            icon = null;
        }
    }

    public float maxDelay = 5f;
    public Image spawnIconPrefab;
    private Queue<MapSpawnData> spawnDatas;

    protected override void Start() {
        base.Start();
        spawnDatas = new Queue<MapSpawnData>();
    }

    public void AddSpawnData(float time, HordeDifficulty difficulty) {
        Color color = new Color();
        switch (difficulty) {
            case HordeDifficulty.Easy:
                color = Color.yellow;
                break;
            case HordeDifficulty.Normal:
                color = new Color(1, 0.5f, 0);
                break;
            case HordeDifficulty.Hard:
                color = Color.red;
                break;
        }
        spawnDatas.Enqueue(new MapSpawnData(time, color));
    }

    private void Update() {
        while(spawnDatas.Count > 0 && spawnDatas.Peek().time <= Time.time) {
            MapSpawnData spawnData = spawnDatas.Dequeue();
            if(spawnData.icon != null) 
                Destroy(spawnData.icon.gameObject);
        }
        foreach(MapSpawnData spawnData in spawnDatas) {
            float timeToSpawn = spawnData.time - Time.time;
            if (timeToSpawn < maxDelay) {
                if (spawnData.icon == null) {
                    spawnData.icon = Instantiate(spawnIconPrefab, Icon.rectTransform);
                    spawnData.icon.color = spawnData.color;
                }
                spawnData.icon.rectTransform.localScale = Vector3.one * (timeToSpawn / maxDelay);
            }
        }
        
    }
}
