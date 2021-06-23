//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NexusTutorial : Unit{

//    protected override void Die() {
//        GameManager gm = GameManager.Instance;
//        gm.resetHorde();
//        health = 5;
//        dead = false;
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusTutorial : Unit {
    protected override void Die() {
        GameManager gm = GameManager.Instance;
        health = 5;
        dead = false;
        gm.resetHorde();
    }
}
