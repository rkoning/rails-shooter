using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Weapon weapon;

    public bool onlyAttackFromFront = true;

    public bool leadPlayer = false;
    public float leadDistance = 12f;

    public bool followPlayer = false;
    public float followSpeed;
    private Vector3 followPoint;

    private void OnEnable() {
        if (!weapon || !TryGetComponent(out weapon)) {
            Debug.LogWarning($"No weapon assigned to AttackPlayer component: {name}");
        }
    }

    private void Update() {
        if (!weapon)
            return;

        bool inFront = Vector3.Dot(RailsMovement.Player.ship.forward, -GetDirectionToPlayer()) < 0;
        // Debug.DrawLine(RailsMovement.Player.ship.position, -GetDirectionToPlayer() * 100, Color.yellow);
        // Debug.DrawLine(RailsMovement.Player.ship.position, RailsMovement.Player.ship.forward * 100, inFront ? Color.cyan : Color.red);

        if (onlyAttackFromFront && Vector3.Dot(RailsMovement.Player.ship.forward, -GetDirectionToPlayer()) < 0) {
            return;
        }

        if (weapon.CanFire()) {
            Vector3 targetPoint = RailsMovement.Player.ship.position;
            if (leadPlayer) {
                targetPoint += RailsMovement.Player.ship.rotation * (Vector3.forward * leadDistance);
            }
            weapon.Fire(targetPoint);
            followPoint = targetPoint;
        }

        if (followPlayer) {
            Vector3 directionToPlayer = (RailsMovement.Player.ship.position - followPoint).normalized;

            followPoint += directionToPlayer * followSpeed * Time.deltaTime;
            weapon.focusPoint = followPoint;
        }
    }

    private Vector3 GetDirectionToPlayer() {
        return (RailsMovement.Player.ship.position - transform.position).normalized;
    }
}
