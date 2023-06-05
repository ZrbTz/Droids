using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopDetailAdapter : MonoBehaviour {
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public TextMeshProUGUI costText;
    public Button buildButton;
    public ShopItemData item;

    private ShopUI shopUI;

    public void Set(ShopUI shopUI, ShopItemData item) {
        this.shopUI = shopUI;
        this.item = item;

        nameText.text = item.displayName;
        iconImage.sprite = item.icon;
        costText.text = item.cost.ToString();
        buildButton.interactable = (GameManager.Instance.coins >= item.cost);
    }

    public void ClickBuild() {
        shopUI.Build();
    }
}
