using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private NexusHealthUI nexusHealthUI;

    public void UpdateNexusHealth(float currentHealth, float maxHealth)
    {
        float percentage = currentHealth / maxHealth;
        nexusHealthUI.SetNexusHealth(percentage);
    }

    public void UpdateHordeNumber(int horde)
    {
        nexusHealthUI.SetHordeNumber(horde);
    }
}
