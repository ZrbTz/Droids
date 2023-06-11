using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthCoinsUI : MonoBehaviour {
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI healthText;

    private void Start() {
        var playerUnit = GameObject.FindWithTag("Player").GetComponent<PlayerUnit>();
        coinsText.text = GameManager.Instance.coins.ToString("N0");
        GameManager.Instance.CoinsChanged += UpdateCoinsText;
        healthText.text = playerUnit.health.ToString();
        playerUnit.HealthChanged += UpdateHealthText;
    }

    private void UpdateCoinsText(int oldCoins, int newCoins) {
        coinsText.text = newCoins.ToString("N0");
    }

    private void UpdateHealthText(float oldHealth, float newHealth) {
        healthText.text = newHealth.ToString();
    }
}
