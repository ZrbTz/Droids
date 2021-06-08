using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image enemyHealthBar;

    public void SetEnemyHealth(float percentage)
    {
        enemyHealthBar.fillAmount = percentage;
    }
}
