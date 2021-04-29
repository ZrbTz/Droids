using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {
    private static MiniMap instance;

    public static MiniMap Instance { get => instance; }

    private HashSet<MiniMapEntity> entities;

    private Vector3 bottomLeftCorner;
    private Vector3 topRightCorner;

    private Image image;
    private RectTransform rectTransform;
    private float width;
    private float length;
    private float widthScale;
    private float lengthScale;
    private Vector2 center;

    private void Awake() {
        entities = new HashSet<MiniMapEntity>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        instance = this;
    }

    private void Start() {
        bottomLeftCorner = Map.Instance.BottomLeftCorner;
        topRightCorner = Map.Instance.TopRightCorner;
        float worldWidth = topRightCorner.x - bottomLeftCorner.x;
        float worldLength = topRightCorner.z - bottomLeftCorner.z;
        float imageRatio = rectTransform.rect.width / rectTransform.rect.height;
        float worldRatio = worldWidth / worldLength;
        Vector2 imageScale = rectTransform.localScale;
        if (worldRatio >= imageRatio) {
            width = rectTransform.rect.width;
            length = width / worldRatio;
            imageScale.y = length / rectTransform.rect.height;
        } else {
            length = rectTransform.rect.height;
            width = length * worldRatio;
            imageScale.x = width / rectTransform.rect.width;
        }
        widthScale = width / worldWidth;
        lengthScale = length / worldLength;
        center = new Vector2(width / 2, length / 2);
    }

    private void LateUpdate() {
        foreach(var entity in entities) {
            Vector3 position = entity.transform.position; position.y = 0;
            Vector3 iconRotation = entity.Icon.rectTransform.eulerAngles;
            float x = (position.x - bottomLeftCorner.x) * widthScale;
            float y = (position.z - bottomLeftCorner.z) * lengthScale;
            iconRotation.z = -entity.transform.eulerAngles.y;
            entity.Icon.rectTransform.localPosition = new Vector2(x, y) - center;
            entity.Icon.rectTransform.eulerAngles = iconRotation;
        }
    }

    public void AddEntity(MiniMapEntity entity) {
        entities.Add(entity);
        entity.Icon.transform.SetParent(transform);
    }

    public void RemoveEntity(MiniMapEntity entity) {
        entities.Remove(entity);
    }
}
