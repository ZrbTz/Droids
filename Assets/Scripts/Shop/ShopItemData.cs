using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item")]
public class ShopItemData : ScriptableObject {
    public string displayName;
    public string description;
    public Sprite icon;
    public GameObject prefab;
    public int cost;
}
