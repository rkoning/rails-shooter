using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 100f;
    public bool destroyOnImpact = true;
    public float initialForce = 100f;

    private Rigidbody rb;

    public delegate void ProjectileHitAnyEvent(Collision collision);
    public delegate void ProjectileHitHealthEvent(Health health);

    public event ProjectileHitAnyEvent OnProjectileHitAny;
    public event ProjectileHitHealthEvent OnProjectileHitHealth;

    private void OnEnable() {      
        StartCoroutine(DisableAfter(lifetime));
        if (rb || TryGetComponent(out rb)) {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
        if (destroyOnImpact) {
            OnProjectileHitAny += Disable;
        }
    }

    public void FireAt(Vector3 position) {
        Vector3 direction = position - transform.position;
        if (rb || TryGetComponent(out rb))
            rb.AddForce(direction * initialForce);

        Debug.DrawRay(transform.position, direction * 80, Color.red, 2f);
    }

    private void OnDisable() {
        if (destroyOnImpact) {
            OnProjectileHitAny -= Disable;
        }
    }

    private void Disable(Collision _) {
        gameObject.SetActive(false);
    }

    private IEnumerator DisableAfter(float duration) {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision c) {
        OnProjectileHitAny?.Invoke(c);
        if (c.collider.TryGetComponent(out Health h)) {
            OnProjectileHitHealth?.Invoke(h);
        }
    }
}
