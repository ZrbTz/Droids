using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNexus : Unit{

    protected override void Die() {
        GameManager gm = GameManager.Instance;
        gm.gameLost();
    }
}
