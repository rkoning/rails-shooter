using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public void OnEnable() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Destroy(gameObject);
    }
}
