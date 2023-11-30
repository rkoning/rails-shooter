using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : WeaponEffect
{
    public LineRenderer beam;
    public float duration;
    public float range;
    public LayerMask mask;

    private float start = float.MinValue;

    public override void Activate()
    {
        StartCoroutine(DoBeam());
    }

    private IEnumerator DoBeam() {
        start = Time.fixedTime;
        beam.enabled = true;
        do {
            Vector3 end;
            Ray ray = new Ray(transform.position, transform.forward);
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
