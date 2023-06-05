using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour {
    public TextMeshProUGUI coinsText;

    private void Start() {
        coinsText.text = GameManager.Instance.coins.ToString("N0");
        GameManager.Instance.CoinsChanged += UpdateCoinsText;
    }

    private void UpdateCoinsText(int oldCoins, int newCoins) {
        coinsText.text = newCoins.ToString("N0");
    }
}
