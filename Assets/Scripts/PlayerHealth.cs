using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public static int PlayerLives;
    private bool isDead;

    public override void OnEnable()
    {
        base.OnEnable();
        isDead = false;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
    }

    public override void Die() {
        PlayerLives--;
        isDead = true;
        RailsMovement.Player.SpawnShipAfter(3);
        base.Die();
        RailsMovement.Player.KillShip();
    }
}
