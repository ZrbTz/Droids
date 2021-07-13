using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAnimatorEventsFix : MonoBehaviour {
    [SerializeField] private Bomber bomber;

    private void Shoot() {
        bomber.Shoot();
        this.GetComponent<EnemyAnimationSounds>().Impact();
    }

    private void StartMarching() {
        bomber.StartMarching();
    }
}
