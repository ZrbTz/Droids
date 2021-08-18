using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoDeboleBoss : ResourceActivable
{
    [SerializeField]
    public Boss_Livello4 boss;

    public override void Activate()
    {
        //boss.stun();
        boss.danneggia();
    }
}
