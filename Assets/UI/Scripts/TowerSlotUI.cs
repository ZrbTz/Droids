using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image icon;

    public void AddIcon(Sprite sprite)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = sprite;
    }

    public void RemoveIcon()
    {
        icon.gameObject.SetActive(false);
    }
}
