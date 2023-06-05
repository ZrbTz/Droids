using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemAdapter : MonoBehaviour {
    public Animator animator;
    public TextMeshProUGUI nameText;
    public ShopItemData item;

    private ShopUI shopUI;

    public void Set(ShopUI shopUI, ShopItemData item) {
        this.shopUI = shopUI;
        this.item = item;

        nameText.text = item.displayName;
    }

    public void ClickSelect() {
        shopUI.SelectItem(this, item);
    }

    public void ShowHighlight() {
        animator.Play("Highlighted");
    }

    public void ClearHighlight() {
        animator.Play("Normal");
    }
}
