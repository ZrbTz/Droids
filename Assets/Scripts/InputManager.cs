using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }

    void Update()
    {
        GetInventoryInput();
    }

    void GetInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.SelectSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.SelectSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.SelectSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.SelectSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventory.SelectSlot(4);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            inventory.SelectNextSlot();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            inventory.SelectPreviousSlot();
        }

        if (Input.GetMouseButtonDown(0))
        {
            inventory.UseSelectedItem();
        }
    }
}
