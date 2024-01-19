using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeaponOnCooldown : MonoBehaviour
{
    Weapon weapon;
    public float startDelay;
    private float startAt;

    private void Start() {
        startAt = Time.fixedTime + startDelay;
        weapon = GetComponent<Weapon>();
        
    }

    private void Update() {
        if (Time.fixedTime > startAt && weapon.CanFire())
            weapon.Fire();
    }
}
