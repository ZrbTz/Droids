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
    [SerializeField]
    private KeyInfosUI keyInfosUI;
    [SerializeField]
    private EnemyHealthUI enemyHealthUI;
    [SerializeField]
    private WonMenuUI wonMenuUI;
    [SerializeField]
    private LostMenuUI lostMenuUI;
    [SerializeField]
    private SuggestionsUI suggestionsUI;
    [SerializeField]
    private GameObject startWaveUI;
    [SerializeField]
    private NotificationUI notificationUI;
    [SerializeField]
    private ShopUI shopUI;

    private bool isInMenu;
    public bool IsInMenu { get => isInMenu; }
    private GameObject currentMenu;

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

    public void UpdateShotgunCooldown(float cooldown)
    {
        weaponTypeUI.SetShotgunCooldown(cooldown);
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
        ShowCursor();
    }

    public void HidePauseMenu()
    {
        pauseMenuUI.gameObject.SetActive(false);
        HideCursor();
    }


    public void ShowClickButton(string key)
    {
        keyInfosUI.ShowClickButton(key);
    }

    public void ShowHoldingButton(string key)
    {
        keyInfosUI.ShowHoldingButton(key);
    }

    public void UpdateHoldingButton(float value)
    {
        keyInfosUI.UpdateHoldingButton(value);
    }

    public void HideClickButton()
    {
        keyInfosUI.HideClickButton();
    }

    public void HideHoldingButton()
    {
        keyInfosUI.HideHoldingButton();
    }

    public void ShowEnemyHealth(float percentage, string name, Sprite icon)
    {
        enemyHealthUI.gameObject.SetActive(true);
        enemyHealthUI.SetEnemyHealth(percentage, name, icon);
    }

    public void HideEnemyHealth()
    {
        enemyHealthUI.gameObject.SetActive(false);
    }

    public void UpdateDashNumber(int number)
    {
        dashCooldownUI.SetDashNumber(number);
    }

    public void ShowLostMenu()
    {
        lostMenuUI.gameObject.SetActive(true);
    }

    public void ShowWonMenu()
    {
        wonMenuUI.gameObject.SetActive(true);
    }

    public void SwitchSuggestionWindow()
    {
        suggestionsUI.SwitchWindow();
    }

    public void UpdateSuggestion(string title, string description)
    {
        suggestionsUI.SetSuggestion(title, description);
    }

    public void ShowSuggestion()
    {
        suggestionsUI.ShowSuggestion();
    }

    public void FullHideSuggestion()
    {
        suggestionsUI.FullHideSuggestion();
    }

    public void ShowStartWave()
    {
        startWaveUI.SetActive(true);
    }

    public void HideStartWave()
    {
        startWaveUI.SetActive(false);
    }

    public void UpdateNotification(string text)
    {
        notificationUI.SetNotification(text);
    }

    private void OpenMenu(GameObject menu) {
        menu.SetActive(true);
        isInMenu = true;
        currentMenu = menu;
        GameManager.Instance.DisableInput();
        ShowCursor();
    }

    public void OpenShopMenu(Merchant merchant) {
        shopUI.Set(merchant);
        OpenMenu(shopUI.gameObject);
    }

    public void CloseCurrentMenu() {
        currentMenu.gameObject.SetActive(false);
        isInMenu = false;
        GameManager.Instance.EnableInput();
        HideCursor();
    }

    private void ShowCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void HideCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
