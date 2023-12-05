using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public delegate void DamageTakenEvent(float amount);
    public event DamageTakenEvent OnDamageTaken;

    public delegate void DeathEvent();
    public event DeathEvent OnDeath;
    
    public virtual void OnEnable() {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount) {
        currentHealth -= amount;
        OnDamageTaken?.Invoke(amount);
        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        OnDeath?.Invoke();
    }
}
