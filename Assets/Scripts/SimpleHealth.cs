using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHealth : Health
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
