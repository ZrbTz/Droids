using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private NexusHealthUI nexusHealthUI;
    [SerializeField]
    private GrenadeSlotUI grenadeSlotUI;
    [SerializeField]
    private WeaponTypeUI weaponTypeUI;
    [SerializeField]
    private DashCooldownUI dashCooldownUI;
    [SerializeField]
    private TowerSlotUI towerSlotUI;
    [SerializeField]
    private PauseMenuUI pauseMenuUI;

    public void UpdateNexusHealth(float currentHealth, float maxHealth)
    {
        float percentage = currentHealth / maxHealth;
        nexusHealthUI.SetNexusHealth(percentage);
    }

    public void UpdateHordeNumber(int horde)
    {
        nexusHealthUI.SetHordeNumber(horde);
    }

    public void UpdateGrenadeAmount(int amount)
    {
        grenadeSlotUI.SetGrenadeAmount(amount);
    }

    public void UpdateWeaponType(bool weaponSelector)
    {
        if (weaponSelector)
        {
            weaponTypeUI.ShowRifleIcon();
        }
        else
        {
            weaponTypeUI.ShowShotgunIcon();
        }
    }

    public void UpdateDashCooldown(float cooldown)
    {
        dashCooldownUI.SetDashCooldown(cooldown);
    }

    public void AddTowerIcon(Sprite sprite)
    {
        towerSlotUI.AddIcon(sprite);
    }

    public void RemoveTowerIcon()
    {
        towerSlotUI.RemoveIcon();
    }

    public void ShowPauseMenu()
    {
        pauseMenuUI.gameObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenuUI.gameObject.SetActive(false);
    }
}
