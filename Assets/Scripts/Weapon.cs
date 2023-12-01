using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 1f;
    public float damage = 10f;
    private float nextFire = float.MinValue;
    private float cooldown;

    public FireMode initialFireMode;

    public List<WeaponEffect> effects;
    public Vector3 focusPoint;
    public LayerMask focusMask;
    public float focusDistance = 200f;

    private void Start() {
        cooldown = fireRate / 1f;
        foreach(var fm in GetComponents<FireMode>()) {
            fm.Initialize(this);
        }

        effects = new(GetComponentsInChildren<WeaponEffect>());
        foreach(var we in effects) {
            we.Initialize(this);
        }
    }

    public void Fire() {
        float now = Time.fixedTime;
        if (now > nextFire) {
            nextFire = now + cooldown;
            initialFireMode.Activate();
        }
    }

    private void Update() {
        SetFocusPoint();
    }

    public void DealDamage(Health health) {
        DealDamage(health, 1f);
    }

    public void DealDamage(Health health, float increment) {
        health.TakeDamage(damage * increment);
    }

    private void SetFocusPoint() {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hit, focusDistance, focusMask)) {
            focusPoint = hit.point;
        } else {
            focusPoint = ray.GetPoint(focusDistance);
        }
    }
}
