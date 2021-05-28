using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAnimatorEventsFix : MonoBehaviour {
    [SerializeField] private NewBomber bomber;

    private void Shoot() {
        bomber.Shoot();
    }

    private void StartMarching() {
        bomber.StartMarching();
    }
}
