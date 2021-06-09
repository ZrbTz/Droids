using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Inventory inventory;
    //public KeyCode placeTurret = KeyCode.E;
    //public KeyCode throwSecondary = KeyCode.Q;
    private GameManager gameManager;

    // Weapon Inputs
    public bool WeaponSwitched { get; set; }
    public bool WeaponFire { get; set; }
    public bool WeaponFireDown { get; set; }

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {
        GetPauseMenuInput();
        if (!gameManager.IsPaused())
        {
            GetInventoryInput();
            GetDashInput();
            GetWeaponInput();
        }
    }

    private void GetInventoryInput()
    {
        if (!this.GetComponent<InteractionController>().TryInteracting() && Input.GetButtonDown("Interact"))
        {
            if (inventory.UseItem(0))
            {
                return;
            }
        }
        if (Input.GetButtonDown("Throw"))
        {
            if (inventory.ShowThrowableTrajectory(1))
            {
                return;
            }
        }
        if (Input.GetButtonUp("Throw"))
        {
            if (inventory.UseItem(1))
            {
                return;
            }
        }
    }

    private void GetDashInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.GetComponent<ThirdPersonControllerDash>().Dash();
        }
    }

    private void GetPauseMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.HandlePause();
        }
    }

    private void GetWeaponInput()
    {
        if (Input.GetButtonDown("WeaponSwitch"))
        {
            WeaponSwitched = true;
        }
        else
        {
            WeaponSwitched = false;
        }

        if (Input.GetButton("Fire1"))
        {
            WeaponFire = true;
        }
        else
        {
            WeaponFire = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            WeaponFireDown = true;
        }
        else
        {
            WeaponFireDown = false;
        }
    }
}
