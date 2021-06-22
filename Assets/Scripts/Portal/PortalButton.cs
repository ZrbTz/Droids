using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : Interactable {
    public Portal portal;
    public float cooldown;

    public Material greenMaterial;
    public Material redMaterial;

    public Renderer buttonRenderer;
    public Renderer lightRenderer;

    public override void Interact(GameObject player) {
        if (Time.time - portal.time < cooldown)
            return;
        portal.Activate();
        buttonRenderer.material = redMaterial;
        lightRenderer.material = redMaterial;
        Invoke(nameof(SetGreenMaterial), cooldown);
    }

    private void SetGreenMaterial() {
        buttonRenderer.material = greenMaterial;
        lightRenderer.material = greenMaterial;
    }
}
