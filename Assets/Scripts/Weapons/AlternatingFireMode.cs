using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingFireMode : FireMode
{
    int current = 0;

    public override void Activate()
    {
        weapon.effects[current].Activate();
        current++;
        if (current >= weapon.effects.Count)
            current = 0;
    }
}
