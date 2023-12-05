using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public Health health;
    public Renderer rend;
    public Material damageFlashMat;
    private Material originalMaterial;

    private Coroutine flashing;

    void OnEnable()
    {
        if (!health && !TryGetComponent<Health>(out health)) {
            Debug.LogWarning($"Health not assigned to DamageFlash: {name}");
        }

        health.OnDamageTaken += Flash;

        if (!rend && !TryGetComponent<Renderer>(out rend)) {
            Debug.LogWarning($"Renderer not assigned to DamageFlash: {name}");
        }

        originalMaterial = rend.material;

        if (!damageFlashMat) {
            Debug.LogWarning($"Material not assigned to DamageFlash: {name}");
        }
    }

    private void Flash(float damage) {
        flashing ??= StartCoroutine(DoFlash());
    }

    private IEnumerator DoFlash() {
        rend.material = damageFlashMat;
        Color originalColor = damageFlashMat.color;
        yield return new WaitForSeconds(.15f);
        rend.material.color = Color.white;
        yield return new WaitForSeconds(.15f);
        rend.material.color = originalColor;
        yield return new WaitForSeconds(.15f);
        rend.material = originalMaterial;
        flashing = null;
    }
}
