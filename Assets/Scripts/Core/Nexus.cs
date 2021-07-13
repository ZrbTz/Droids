using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Unit{

    protected override void Die() {
        GameManager gm = GameManager.Instance;
        gm.gameLost();
    }
}
