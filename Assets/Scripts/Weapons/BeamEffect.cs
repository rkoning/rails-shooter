using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : WeaponEffect
{
    public LineRenderer beam;
    public float duration;
    public float range;
    public LayerMask mask;

    public override void Activate()
    {
        StartCoroutine(DoBeam());
    }

    private IEnumerator DoBeam() {
        float start = Time.fixedTime;
        beam.enabled = true;
        do {
            Vector3 end;
            Vector3 focusDirection = (weapon.focusPoint - transform.position).normalized;
            Ray ray = new Ray(transform.position, focusDirection);
            if (Physics.Raycast(ray, out var hit, range, mask)) {
                if (hit.collider.TryGetComponent<Health>(out var health)) {
                    weapon.DealDamage(health, 1f / duration * Time.deltaTime);
                }
                end = hit.point;
            } else {
                end = ray.GetPoint(range);
            }

            beam.SetPositions(new Vector3[] { transform.position, end });
            yield return null;
        } while (Time.fixedTime < start + duration);
        beam.enabled = false;
    }

}
