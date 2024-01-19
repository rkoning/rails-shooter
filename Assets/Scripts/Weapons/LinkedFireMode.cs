using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedFireMode : FireMode
{
    public override void Activate()
    {
        if (next) {
            next.Activate();
            Debug.Log("Has next is activating from LinkedFireMode");
        } else {
            foreach(var we in weapon.effects) {
                Debug.Log($"Activating effect: {we}");

                we.Activate();
            }
        }
    }
}
