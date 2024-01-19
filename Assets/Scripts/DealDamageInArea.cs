using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageInArea : MonoBehaviour
{
    [Header("On Entry")]
    public bool dealDamageOnEntry;
    public float entryDamage;

    [Header("Per Tick")]
    public bool dealDamagePerTick;
    public float ticksPerSecond = 8f;
    private float tickTime;
    public float damagePerSecond;
    private float damagePerTick;

    private List<Health> localTargets = new();

    private void Start() {
        tickTime = 1f / ticksPerSecond;
        damagePerTick = damagePerSecond * tickTime;
    }

    private void Update() {
        if (!dealDamagePerTick) 
            return;

        if (Time.fixedTime % tickTime == 0)
            foreach(var h in localTargets)
                h.TakeDamage(damagePerTick);
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        if (other.TryGetComponent(out Health h)) {
            localTargets.Add(h);
            if (dealDamageOnEntry)
                h.TakeDamage(entryDamage);
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out Health h) && localTargets.Contains(h)) {
            localTargets.Remove(h);
        }
    }
}
