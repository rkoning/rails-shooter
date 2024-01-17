using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireMode : MonoBehaviour
{
    public FireMode next;
    public Weapon weapon;

    public virtual void Initialize(Weapon weapon) {
        this.weapon = weapon;
    }

    public abstract void Activate();
}
