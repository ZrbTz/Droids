using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapEntity : MonoBehaviour {
    [SerializeField] private Image iconPrefab;
    private Image icon;
    public bool isPlayer = false;

    protected virtual void Start() {
        icon = Instantiate(iconPrefab);
        MiniMap.Instance.AddEntity(this);
    }

    public Image Icon { get => icon; }

    private void OnDestroy() {
        MiniMap.Instance.RemoveEntity(this);
        if(icon != null)
            Destroy(icon.gameObject);
    }
}
