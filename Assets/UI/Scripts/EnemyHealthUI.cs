using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image enemyHealthBar;
    [SerializeField]
    private UnityEngine.UI.Image enemyIcon;
    [SerializeField]
    private TMPro.TextMeshProUGUI enemyName;

    public void SetEnemyHealth(float percentage, string name, Sprite icon)
    {
        enemyHealthBar.fillAmount = percentage;
        enemyIcon.sprite = icon;
        enemyName.SetText(name);
    }
}
