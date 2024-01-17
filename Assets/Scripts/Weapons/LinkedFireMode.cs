using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedFireMode : FireMode
{
    public override void Activate()
    {
        if (next) {
            next.Activate();
        } else {
            foreach(var we in weapon.effects) {
                we.Activate();
            }
        }
    }
}
