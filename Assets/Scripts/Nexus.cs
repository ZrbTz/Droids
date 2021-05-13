using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Unit{

    protected override void Die() {
        Debug.Log("ma ke ci faccio qui");
        GameManager gm = GameManager.Instance;
        gm.gameLost();
    }
}
