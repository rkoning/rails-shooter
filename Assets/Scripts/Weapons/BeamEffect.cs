using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : WeaponEffect
{
    public LineRenderer beam;
    public float duration;
    public float range;
    public LayerMask mask;
    public int penetration = 0;
    public float radius = 0.25f;

    private void Start() {
        beam.enabled = false;
    }
    
    public override void Activate()
    {
        StartCoroutine(penetration > 0 ? DoPenetratingBeam() : DoBeam());
    }

    private IEnumerator DoBeam() {
        float start = Time.fixedTime;
        beam.enabled = true;
        do {
            Vector3 end;
            Vector3 focusDirection = (weapon.focusPoint - transform.position).normalized;
            Ray ray = new Ray(transform.position, focusDirection);
            if (Physics.SphereCast(ray, radius, out var hit, range, mask)) {
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

    private IEnumerator DoPenetratingBeam() {
        float start = Time.fixedTime;
        beam.enabled = true;
        do {
            Vector3 end;
            Vector3 focusDirection = (weapon.focusPoint - transform.position).normalized;
            Ray ray = new Ray(transform.position, focusDirection);
            RaycastHit[] hits = Physics.SphereCastAll(ray, radius, range, mask);

                end = ray.GetPoint(range);

            if (hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++) {
                    if (hits[i].collider.TryGetComponent<Health>(out var health)) 
                        weapon.DealDamage(health, 1f / duration * Time.deltaTime);
                    
                    if (i >= penetration) {
                        end = hits[i].point;
                        break;
                    }
                }
            }

            beam.SetPositions(new Vector3[] { transform.position, end });
            yield return null;
        } while (Time.fixedTime < start + duration);
        beam.enabled = false;
    }

}
