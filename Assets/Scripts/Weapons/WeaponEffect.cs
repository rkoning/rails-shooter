using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEffect : MonoBehaviour
{
    protected Weapon weapon;
    
    public virtual void Initialize(Weapon weapon) {
        this.weapon = weapon;
    }
    public abstract void Activate();
}
