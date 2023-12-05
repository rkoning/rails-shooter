using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnImpact : MonoBehaviour
{
    public Health Health;
    public LayerMask DamagingLayers;
    public float Damage;

    public float Delay;
    private float lastCollision = float.MinValue;

    private void OnEnable() {
        if (Health == null && !TryGetComponent(out Health)) {
            Debug.LogWarning($"No Health component assigned to DamageOnImpact: {name}");
        }
    }
    public void OnCollisionEnter(Collision collision) {
        if (
            (DamagingLayers & (1 << collision.gameObject.layer)) != 0 &&
            lastCollision + Delay < Time.fixedTime
        ) {
            lastCollision = Time.fixedTime;
            Health.TakeDamage(Damage);
        }
    }
}
