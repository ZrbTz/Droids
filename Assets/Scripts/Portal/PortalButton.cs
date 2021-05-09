using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : Interactable {
    public Portal portal;
    public float cooldown;

    public Material greenMaterial;
    public Material redMaterial;

    private new Renderer renderer;

    private void Start() {
        renderer = GetComponent<Renderer>();
    }

    public override void Interact(GameObject player) {
        if (Time.time - portal.time < cooldown)
            return;
        portal.Activate();
        renderer.material = redMaterial;
        Invoke(nameof(SetGreenMaterial), cooldown);
    }

    private void SetGreenMaterial() {
        renderer.material = greenMaterial;
    }
}
