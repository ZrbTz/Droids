using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Unit
{
    protected override void Die()
    {
        Destroy(gameObject, 0.0f);
    }
}
