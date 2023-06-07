using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour {
    public Transform listRoot;
    public ShopItemAdapter itemAdapterPrefab;
    public ShopDetailAdapter detailAdapter;

    private Merchant merchant;
    private ShopItemAdapter selectedAdapter;
    private ShopItemData selectedItem;

    private ShopItemAdapter defaultAdapter;

    public void Set(Merchant merchant) {
        this.merchant = merchant;
        foreach (Transform child in listRoot) {
            Destroy(child.gameObject);
        }
        defaultAdapter = null;
        foreach (var item in merchant.shopItems) {
            var adapter = Instantiate(itemAdapterPrefab, listRoot);
            if (defaultAdapter == null) {
                defaultAdapter = adapter;
            }
            adapter.Set(this, item);
        }
    }

    private void OnEnable() {
        defaultAdapter.ClickSelect();
    }

    public void SelectItem(ShopItemAdapter adapter, ShopItemData item) {
        if (selectedAdapter != null) {
            selectedAdapter.ClearHighlight();
        }
        selectedAdapter = adapter;
        selectedItem = item;
        selectedAdapter.ShowHighlight();
        detailAdapter.Set(this, item);
    }

    public void Build() {
        GameManager.Instance.coins -= selectedItem.cost;
        merchant.SpawnTurret(selectedItem);
        gameObject.SetActive(false);
    }
}
