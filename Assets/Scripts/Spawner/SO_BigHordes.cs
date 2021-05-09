using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Horde", menuName = "ScriptableObjects/Hordes")]
public class SO_BigHordes : ScriptableObject{

    public enum HordeDifficulty{
        None, Easy, Normal, Hard
    }
    
    [System.Serializable]
    public class Horde{
        public GameObject enemy;
        public int count;
        public float delay;
        public float tempoPerSpawnare;
        public HordeDifficulty difficulty;
    }

    public BigHorde[] bigHordes;
}
